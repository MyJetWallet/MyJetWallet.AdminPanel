using System;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Linq;
using System.Threading.Tasks;
using Backoffice.Abstractions.Models;
using Backoffice.Abstractions.Services;
using DotNetCoreDecorators;
using Microsoft.AspNetCore.Http;
using MyCrm.Accounts.Grpc;
using MyCrm.Accounts.Grpc.Contracts;
using MyCrm.Deposits.Grpc;
using MyCrm.Deposits.Grpc.Contracts;
using MyCrm.Deposits.Grpc.Contracts.PaymentProviderSettings;
using MyCrm.Deposits.Grpc.Contracts.PaymentSystemSettings;
using MyCrm.Deposits.Grpc.Models;
using SimpleTrading.Abstraction.Payments;
using SimpleTrading.Deposit.Grpc;
using SimpleTrading.Deposit.Grpc.Contracts;

namespace Backoffice.Services.Deposits
{
    public class DepositService : IDepositService
    {
        private IMyCrmDepositsGrpcService DatasourceService { get; set; }
        private IDepositManagerGrpcService DepositManager { get; set; }
        private IMyCrmAccountsGrpcService AccountsGrpcService { get; set; }
        private IHttpContextAccessor HttpContextAccessor { get; set; }

        public DepositService(IMyCrmDepositsGrpcService crmDataSource, IDepositManagerGrpcService depositManager,
            IMyCrmAccountsGrpcService accountsGrpcService, IHttpContextAccessor ctx)
        {
            DatasourceService = crmDataSource;
            DepositManager = depositManager;
            AccountsGrpcService = accountsGrpcService;
            HttpContextAccessor = ctx;
        }

        public ValueTask<IReadOnlyList<IDepositModel>> GetSuccessfulAsync(string boUserId, DateTime from, DateTime to,
            bool includeInternal)
        {
            return GetAsync(new SearchDepositDto
            {
                BoUserId = boUserId,
                From = from,
                To = to,
                IncludeInternal = includeInternal,
                Statuses = new[] { CrmDepositStatus.Paid }
            });
        }

        public ValueTask<IReadOnlyList<IDepositModel>> GetAllAsync(string boUserId, DateTime from, DateTime to,
            bool includeInternal)
        {
            return GetAsync(new SearchDepositDto
            {
                BoUserId = boUserId,
                From = from,
                To = to,
                IncludeInternal = includeInternal,
                Statuses = new[] { CrmDepositStatus.Paid, CrmDepositStatus.Failed, CrmDepositStatus.Registered }
            });
        }

        public async ValueTask<IReadOnlyList<IDepositModel>> GetAsync(ISearchDepositModel searchDepositModel)
        {
            var request = new GetByPeriodGrpcRequest
            {
                BackOfficeUserId = searchDepositModel.BoUserId,
                FromDate = searchDepositModel.From,
                ToDate = searchDepositModel.To,
                Statuses = searchDepositModel.Statuses.Select(itm => itm.ToDomain()).ToArray(),
                IncludeInternalTraders = searchDepositModel.IncludeInternal
            };

            return await DatasourceService.GetByPeriodAsync(request)
                .SelectAsync(itm => itm.ToDomain())
                .AsListAsync();
        }

        public async ValueTask<IReadOnlyList<IDepositModel>> GetByTraderIdAsync(string boUserId, string traderId,
            bool successfulOnly)
        {
            var statuses = successfulOnly
                ? new[] { DepositStatus.Paid }
                : new[] { DepositStatus.Paid, DepositStatus.Registered, DepositStatus.Failed };

            var request = new GetByTraderIdGrpcRequest
            {
                BackOfficeUserId = boUserId,
                TraderId = traderId,
                Statuses = statuses
            };

            return await DatasourceService.GetByTraderIdAsync(request)
                .SelectAsync(itm => itm.ToDomain())
                .AsListAsync();
        }

        public async ValueTask<IReadOnlyList<IDepositModel>> FindAsync(string boUserId, string transactionId)
        {
            var request = new GetByTransactionIdGrpcRequest
            { BackOfficeUserId = boUserId, TransactionId = transactionId };

            return await DatasourceService.FindByTransactionIdAsync(request)
                .SelectAsync(itm => itm.ToDomain())
                .AsListAsync();
        }

        public async ValueTask<IDepositModel> GetAsync(string boUserId, string transactionId)
        {
            var request = new GetByTransactionIdGrpcRequest
            { BackOfficeUserId = boUserId, TransactionId = transactionId };

            return (await DatasourceService.GetByTransactionIdAsync(request)).ToDomain();
        }

        public async ValueTask FundAccount(IFundAccountDto fundRequest)
        {
            var account = await AccountsGrpcService.GetAccountAsync(new GetCrmAccountsGrpcRequest
            {
                TraderId = fundRequest.TraderId,
                BackofficeUserId = await HttpContextAccessor.HttpContext.GetBoUserId(),
                AccountId = fundRequest.AccountId
            });

            var result = await DepositManager.CreatePaymentInvoiceAsync(new CreatePaymentInvoiceGrpcRequest
            {
                PaymentSystemId = fundRequest.PaymentSystemId,
                Currency = account.Currency,
                AccountId = fundRequest.AccountId,
                DepositSum = fundRequest.Amount,
                TraderId = fundRequest.TraderId,
                Author = await HttpContextAccessor.HttpContext.GetBoUserId(),
                Comment = "Created Manually: " + fundRequest.Comment,
                PsCurrency = fundRequest.PsCurrency,
                PsAmount = fundRequest.PsAmount
            });

            await DepositManager.ProcessDepositAsync(new ProcessDepositRequest
            {
                TransactionId = result.TransactionId,
                Author = await HttpContextAccessor.HttpContext.GetBoUserId(),
                Comment = "Processed Manually: " + fundRequest.Comment,
                PaymentInvoiceStatus = PaymentInvoiceStatusEnum.Approved,
                PsTransactionId = fundRequest.PsTransactionId
            });
        }

        public async ValueTask<IReadOnlyList<string>> GetAvailablePaymentProvidersAsync()
        {
            var result =
                await DatasourceService.GetAvailablePaymentProvidersAsync(
                    new GetAvailablePaymentProvidersGrpcRequest());

            return result.AvailablePaymentProviders.AsReadOnlyList();
        }

        public async ValueTask<IReadOnlyList<string>> GetSupportedBrandsAsync()
        {
            var result =
                await DatasourceService.GetSupportedBrandsAsync(new GetSupportedBrandsGrpcRequest());

            return result.BrandNames.AsReadOnlyList();
        }

        public async ValueTask<IReadOnlyList<IPaymentProviderSettingsModel>> GetPaymentProviderSettingsAsync()
        {
            var result =
                await DatasourceService.GetPaymentProvidersStrategySettingsAsync(
                    new GetPaymentProvidersStrategySettingsGrpcRequest());

            return result.ProvidersStrategySettingsGrpcModels?.Select(itm => itm.ToDomain()).AsReadOnlyList() ?? Array.Empty<IPaymentProviderSettingsModel>();
        }

        public async ValueTask SetPaymentProvidersSettingsAsync(IEnumerable<IPaymentProviderSettingsModel> settings)
        {
            await RemovePaymentProviderSettingsModelAsync(settings);
            await UpsertPaymentProviderSettingsModelAsync(settings);
        }

        private async Task RemovePaymentProviderSettingsModelAsync(
            IEnumerable<IPaymentProviderSettingsModel> paymentProvidersSettingsModel)
        {
            foreach (var settingsForDeleting in paymentProvidersSettingsModel.Where(x => x.IsDeleted))
                await DatasourceService.RemovePaymentProvidersStrategySettingsAsync(
                    new RemovePaymentProvidersStrategySettingsGrpcRequest
                    {
                        Brand = settingsForDeleting.Brand,
                        PaymentProviderName = settingsForDeleting.PaymentProviderName
                    });
        }

        private async Task RemovePaymentSystemSettingsModelAsync(
            IEnumerable<IPaymentSystemSettingsModel> models)
        {
            foreach (var settingsForDeleting in models.Where(x => x.IsDeleted))
                await DatasourceService.RemovePaymentSystemSettingsAsync(
                    new RemovePaymentSystemSettingsGrpcRequest
                    {
                        Brand = settingsForDeleting.Brand,
                        PaymentSystemName = settingsForDeleting.PaymentSystemName
                    });
        }

        private async Task UpsertPaymentProviderSettingsModelAsync(
            IEnumerable<IPaymentProviderSettingsModel> paymentProvidersSettingsModel)
        {
            foreach (var paymentProvidersSettings in paymentProvidersSettingsModel.Where(x => !x.IsDeleted))
                await DatasourceService.SetPaymentProvidersStrategySettingsAsync(
                    new SetPaymentProvidersStrategySettingsGrpcRequest
                    {
                        ProvidersStrategySettingsGrpcModel = ProvidersStrategySettingsGrpcModel.Create(
                            paymentProvidersSettings.PaymentProviderName,
                            paymentProvidersSettings.Brand,
                            paymentProvidersSettings.Weight,
                            paymentProvidersSettings.SupportedGeo?.ToList() ?? new List<string>(0),
                            paymentProvidersSettings.RestrictedGeo?.ToList() ?? new List<string>(0))
                    });
        }

        private async Task UpsertPaymentSystemSettingsModelAsync(
            IEnumerable<IPaymentSystemSettingsModel> paymentSystemSettingsModels)
        {
            foreach (var paymentSystemSettings in paymentSystemSettingsModels.Where(x => !x.IsDeleted))
                await DatasourceService.SetPaymentSystemSettingsAsync(
                    new SetPaymentSystemSettingsGrpcRequest
                    {
                        PaymentSystemSettingsGrpcModel = PaymentSystemSettingsGrpcModel.Create(
                            paymentSystemSettings.PaymentSystemName,
                            paymentSystemSettings.Brand,
                            paymentSystemSettings.IsEnable,
                            paymentSystemSettings.SupportedGeo?.ToList() ?? new List<string>(0),
                            paymentSystemSettings.RestrictedGeo?.ToList() ?? new List<string>(0))
                    });
        }

        public async ValueTask<IReadOnlyList<string>> GetAvailablePaymentSystemsAsync()
        {
            var result =
               await DatasourceService.GetAvailablePaymentSystemsAsync(
                   new GetAvailablePaymentSystemsGrpcRequest());

            return result.AvailablePaymentSystems.ToList();
        }

        public async ValueTask<IReadOnlyList<IPaymentSystemSettingsModel>> GetPaymentSystemSettingsAsync()
        {
            var result =
               await DatasourceService.GetPaymentSystemSettingsAsync(
                   new GetPaymentSystemSettingsGrpcRequest());

            return result.PaymentSystemSettingsGrpcModels?.Select(x=>x.ToDomain()).AsReadOnlyList() ?? Array.Empty<IPaymentSystemSettingsModel>();
        }

        public async ValueTask SetPaymentSystemSettingsAsync(IEnumerable<IPaymentSystemSettingsModel> settings)
        {
            await RemovePaymentSystemSettingsModelAsync(settings);
            await UpsertPaymentSystemSettingsModelAsync(settings);
        }
        
        public async ValueTask MakeDepositVoid(string depositId, string comment)
        {
            var boUserId = await HttpContextAccessor.HttpContext.GetBoUserId();
            await DatasourceService.MakeDepositVoidAsync(new MakeDepositVoidRequest
            {
                DepositId = depositId,
                Comment = comment,
                UserId = boUserId,
                Commision = 0
            });
        }
    }
}