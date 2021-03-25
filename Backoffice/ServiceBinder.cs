using Allegiance.Blazor.Highcharts.Services;
using Backoffice.Abstractions.Bo;
using Backoffice.Abstractions.Services;
using Backoffice.RestServices;
using Backoffice.Services;
using Backoffice.Services.Accounts;
using Backoffice.Services.ActiveDeals;
using Backoffice.Services.Affiliates;
using Backoffice.Services.AuditLog;
using Backoffice.Services.AutoOwnerProfiles;
using Backoffice.Services.Backoffice;
using Backoffice.Services.Comments;
using Backoffice.Services.Deposits;
using Backoffice.Services.KYC;
using Backoffice.Services.Logs;
using Backoffice.Services.Online;
using Backoffice.Services.PersonalData;
using Backoffice.Services.PhonePool;
using Backoffice.Services.Status;
using Backoffice.Services.Transactions;
using Backoffice.Services.Withdrawal;
using Backoffice.TableStorage;
using Grpc.Net.Client;
using Microsoft.Extensions.DependencyInjection;
using MyCrm.Accounts.Grpc;
using MyCRM.AccountTransactions.Grpc;
using MyCrm.AffiliateAccess.Grpc;
using MyCrm.AuditLog.Grpc;
using MyCrm.Auth.GrpcContracts;
using MyCrm.AutoOwnerProfiles.Grpc;
using MyCrm.BusinessCategories.Grpc;
using MyCrm.Calls.Grpc;
using MyCrm.Comments.GrpcContracts;
using MyCrm.Deposits.Grpc;
using MyCrm.Kyc.Grpc;
using MyCRM.Logs.GrpcContracts;
using MyCrm.MyCrmTradersUtmParametersGrpcContracts;
using MyCrm.PaymentReport.GrpcContracts;
using MyCrm.PersonalData.Grpc;
using MyCrm.TraderCrmStatuses.Grpc;
using MyCrm.TraderMarketingSalesData.Grpc.Backoffice;
using MyCrm.TraderOnlineData.Grpc;
using MyCrm.TradersDocuments.Grpc;
using MyCrm.Withdrawals.Grpc;
using ProtoBuf.Grpc.Client;
using SimpleTrading.Deposit.Grpc;
using Sotsera.Blazor.Toaster.Core.Models;

namespace Backoffice
{
    public static class ServiceBinder
    {
        public static void BindServices(this IServiceCollection app, SettingsModel settingsModel)
        {
            app.AddScoped<IPersonalDataService, PersonalDataService>();
            app.AddScoped<IAccountService, AccountsService>();
            app.AddScoped<IActiveDealService, ActiveDealsService>();
            app.AddScoped<ILogService, LogsService>();
            app.AddScoped<ITradersKycService, TradersKycService>();
            app.AddScoped<IStatusService, StatusService>();
            app.AddScoped<ITransactionsService, TransactionService>();
            app.AddScoped<IAuditLogService, AuditLogService>();
            app.AddScoped<IPhonePoolService, PhonePoolService>();
            app.AddScoped<IBackofficeOfficeService, BackofficeOfficeService>();
            app.AddScoped<ICommentsService, CommentsService>();
            app.AddScoped<IAffiliateService, AffiliatesService>();
            app.AddScoped<ITradersOnlineService, TraderOnlineService>();
            app.AddScoped<IDepositService, DepositService>();
            app.AddScoped<IWithdrawalService, WithdrawalService>();
            app.AddScoped<IPaymentServiceReport, PaymentReportService>();
            app.AddScoped<IAutoOwnerProfileService, AutoOwnerProfileService>();

            app.AddSingleton<ILastSearchUserCache, LastSearchUserCache>();
            app.AddSingleton<IBoUsersService, BoUsersService>();
            app.AddSingleton(new DepositRestClient(settingsModel.MonfexApi, settingsModel.HpApi, settingsModel.AlianceApi));

            app.AddToaster(config =>
            {
                config.PositionClass = Defaults.Classes.Position.TopRight;
                config.PreventDuplicates = false;
                config.NewestOnTop = true;
            });

            app.AddTransient<IChartService, ChartService>();
        }

        public static void BindGrpcServices(this IServiceCollection app, SettingsModel settings)
        {
            app.AddSingleton(GrpcChannel
                .ForAddress(settings.PersonalDataGrpcHostPort)
                .CreateGrpcService<IMyCrmPersonalDataGrpcService>());

            app.AddSingleton(GrpcChannel
                .ForAddress(settings.AccountsGrpcHostPort)
                .CreateGrpcService<IMyCrmAccountsGrpcService>());

            app.AddSingleton(GrpcChannel
                .ForAddress(settings.TransactionsGrpcHostPort)
                .CreateGrpcService<IMyCrmAccountTransactionsGrpcService>());

            app.AddSingleton(GrpcChannel
                .ForAddress(settings.ActiveDealsGrpcService)
                .CreateGrpcService<IMyCrmActiveDealsGrpcService>());

            app.AddSingleton(GrpcChannel
                .ForAddress(settings.LogsGrpcUrl)
                .CreateGrpcService<IMyCrmLogsGrpcService>());

            app.AddSingleton(GrpcChannel
                .ForAddress(settings.LogsGrpcUrl)
                .CreateGrpcService<IMyCrmLogsGrpcService>());

            app.AddSingleton(GrpcChannel
                .ForAddress(settings.KycGrpcUrl)
                .CreateGrpcService<IMyCrmKycGrpcService>());

            app.AddSingleton(GrpcChannel
                .ForAddress(settings.StatusesGrpcUrl)
                .CreateGrpcService<IMyCrmTraderCrmStatusesGrpcService>());

            app.AddSingleton(GrpcChannel
                .ForAddress(settings.StatusesGrpcUrl)
                .CreateGrpcService<IMyCrmWriterTraderMarketingSalesDataForBackofficeGrpcService>());

            app.AddSingleton(GrpcChannel
                .ForAddress(settings.StatusesGrpcUrl)
                .CreateGrpcService<IMyCrmReaderTraderMarketingSalesDataForBackofficeGrpcService>());

            app.AddSingleton(GrpcChannel
                .ForAddress(settings.LogsGrpcUrl)
                .CreateGrpcService<IMyCrmAuditLogGrpcService>());

            app.AddSingleton(GrpcChannel
                .ForAddress(settings.AuthGrpcServiceUrl)
                .CreateGrpcService<IMyCrmUsersAuthGrpcService>());

            app.AddSingleton(GrpcChannel
                .ForAddress(settings.PhonePoolServiceUrl)
                .CreateGrpcService<IMyCrmPhonePoolGrpcService>());

            app.AddSingleton(GrpcChannel
                .ForAddress(settings.PhonePoolServiceUrl)
                .CreateGrpcService<IMyCrmCallsGrpcService>());

            app.AddSingleton(GrpcChannel
                .ForAddress(settings.AuthGrpcServiceUrl)
                .CreateGrpcService<IMyCrmBusinessCategoriesGrpcService>());

            app.AddSingleton(GrpcChannel
                .ForAddress(settings.AuthGrpcServiceUrl)
                .CreateGrpcService<IMyCrmCommentsGrpcService>());

            app.AddSingleton(GrpcChannel
                .ForAddress(settings.AuthGrpcServiceUrl)
                .CreateGrpcService<IMyCrmAffiliateAccessServiceGrpc>());

            app.AddSingleton(GrpcChannel
                .ForAddress(settings.AuthGrpcServiceUrl)
                .CreateGrpcService<IMyCrmTraderOnlineDataGrpcService>());

            app.AddSingleton(GrpcChannel
                .ForAddress(settings.AuthGrpcServiceUrl)
                .CreateGrpcService<IMyCrmTradersDocumentsGrpcService>());
            
            app.AddSingleton(GrpcChannel
                .ForAddress(settings.AuthGrpcServiceUrl)
                .CreateGrpcService<IMyCrmAutoOwnerProfileGrpcServices>());

            app.AddSingleton(GrpcChannel
                .ForAddress(settings.AuthGrpcServiceUrl)
                .CreateGrpcService<IMyCrmDepositsGrpcService>());

            app.AddSingleton(GrpcChannel
                .ForAddress(settings.DepositManagerGrpcService)
                .CreateGrpcService<IDepositManagerGrpcService>());

            app.AddSingleton(GrpcChannel
                .ForAddress(settings.PhonePoolServiceUrl)
                .CreateGrpcService<IMyCrmWithdrawalsGrpcService>());

            app.AddSingleton(GrpcChannel
                .ForAddress(settings.PhonePoolServiceUrl)
                .CreateGrpcService<IMyCrmTraderUrmParametersGrpcService>());

            app.AddSingleton(GrpcChannel
                .ForAddress(settings.PhonePoolServiceUrl)
                .CreateGrpcService<IMyCrmPaymentsReportGrpcService>());
        }

        public static void BindTableStorages(this IServiceCollection app, SettingsModel settingsModel)
        {
            app.AddSingleton<IBackofficeRolesRepository>(settingsModel.TableStorageConnectionString
                .CreateRolesRepository());

            app.AddSingleton<IBackofficeFiltersPresetRepository>(settingsModel.TableStorageConnectionString
                .CreateFiltersPresetsRepository());
        }
    }
}