using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backoffice.Abstractions.Models;
using Backoffice.Abstractions.Services;
using DotNetCoreDecorators;
using Microsoft.AspNetCore.Http;
using MyCrm.Withdrawals.Grpc;
using MyCrm.Withdrawals.Grpc.Contracts;

namespace Backoffice.Services.Withdrawal
{
    public class WithdrawalService : IWithdrawalService
    {
        private readonly IMyCrmWithdrawalsGrpcService _datasourceService;
        private IHttpContextAccessor _ctx;

        public WithdrawalService(IMyCrmWithdrawalsGrpcService datasourceService, IHttpContextAccessor ctx)
        {
            _datasourceService = datasourceService;
            _ctx = ctx;
        }

        public async ValueTask<IReadOnlyList<IWithdrawalModel>> GetPendingWithdrawsAsync()
        {
            var request = new GetPendingWithdrawsGrpcRequest {BackOfficeUserId = await _ctx.HttpContext.GetBoUserId()};

            return await _datasourceService
                .GetPendingAsync(request)
                .SelectAsync(itm => itm.ToDomain())
                .AsListAsync();
        }

        public async ValueTask<IWithdrawalModel> CancelWithdrawAsync(string transactionId, string comment)
        {
            var request = new ProcessWithdrawGrpcContracts
                {BackOfficeUserId = await _ctx.HttpContext.GetBoUserId(), TransactionId = transactionId, Comment = comment};

            await _datasourceService.CancelWithdrawAsync(request);
            return await GetByIdAsync(transactionId);
        }

        public async ValueTask<IWithdrawalModel> CancelWithReservationAsync(string transactionId, string comment)
        {
            var request = new ProcessWithdrawGrpcContracts
                {BackOfficeUserId = await _ctx.HttpContext.GetBoUserId(), TransactionId = transactionId, Comment = comment};

            await _datasourceService.CancelWithReservationAsync(request);
            return await GetByIdAsync(transactionId);
        }

        public async ValueTask<IWithdrawalModel> ProcessWithdrawAsync(string transactionId, string comment)
        {
            var request = new ProcessWithdrawGrpcContracts
                {BackOfficeUserId = await _ctx.HttpContext.GetBoUserId(), TransactionId = transactionId, Comment = comment};

            await _datasourceService.ProcessWithdrawAsync(request);
            return await GetByIdAsync(transactionId);
        }

        public async ValueTask<IWithdrawalModel> ReserveMoneyAsync(string transactionId, string comment)
        {
            var request = new ProcessWithdrawGrpcContracts
                {BackOfficeUserId = await _ctx.HttpContext.GetBoUserId(), TransactionId = transactionId, Comment = comment};

            var response = await _datasourceService.ReserveWithdrawMoneyAsync(request);

            if (response.Status != ReserveWithdrawalMoneyStatuses.Ok)
                throw new Exception($"Error: {response.Status}");

            return await GetByIdAsync(transactionId);
        }

        public async ValueTask<IReadOnlyList<IWithdrawalModel>> GetWithdrawsAsync(ISearchWithdrawalModel searchModel)
        {
            var request = new GetWithdrawsByPeriodGrpcRequest
            {
                BackOfficeUserId = await _ctx.HttpContext.GetBoUserId(),
                From = searchModel.From,
                To = searchModel.To,
                Statuses = searchModel.Statuses.Select(itm => itm.ToGrpc()).ToArray()
            };

            return await _datasourceService
                .GetByPeriodAsync(request)
                .SelectAsync(itm => itm.ToDomain())
                .AsListAsync();
        }

        public async ValueTask<IWithdrawalModel> GetByIdAsync(string id)
        {
            var request = new GetWithdrawByIdGrpcRequest
                {BackOfficeUserId = await _ctx.HttpContext.GetBoUserId(), TransactionId = id};
            return (await _datasourceService.GetByIdAsync(request)).ToDomain();
        }

        public async ValueTask UpdateWithdrawDetailsAsync(string id, string paymentSystemId,
            string withdrawData)
        {
            var request = new UpdateWithdrawDetailsGrpcRequest
            {
                BackOfficeUserId = await _ctx.HttpContext.GetBoUserId(), Id = id, PaymentSystemData = withdrawData,
                PaymentSystemId = paymentSystemId
            };

            await _datasourceService.UpdateWithdrawDetailsAsync(request);
        }

        public async ValueTask Create(string traderId, string accountId, string currency,
            double amount,
            string type)
        {
            var request = new AddWithdrawalRequest
            {
                TraderId = traderId,
                AccountId = accountId,
                Currency = currency,
                Amount = amount,
                Type = type,
                Data = "{}",
                BoUserId = await _ctx.HttpContext.GetBoUserId()
            };

            await _datasourceService.AddWithdrawalAsync(request);
        }
    }
}