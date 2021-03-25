using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Backoffice.Abstractions.Models;
using Backoffice.Abstractions.Services;
using DotNetCoreDecorators;
using Microsoft.AspNetCore.Http;
using MyCrm.Accounts.Grpc;
using MyCrm.Accounts.Grpc.Contracts;

namespace Backoffice.Services.Accounts
{
    public class AccountsService : IAccountService
    {
        private readonly IHttpContextAccessor _ctx;
        private readonly IMyCrmAccountsGrpcService _dataSource;

        public AccountsService(IHttpContextAccessor ctx, IMyCrmAccountsGrpcService dataSource)
        {
            _ctx = ctx;
            _dataSource = dataSource;
        }

        public async ValueTask<IAccountModel> GetAsync(string traderId, string accountId)
        {
            var id = await _ctx.HttpContext.GetBoUserId();
            var request = new GetCrmAccountsGrpcRequest
                {TraderId = traderId, BackofficeUserId = id, AccountId = accountId};

            return (await _dataSource.GetAccountAsync(request)).ToDomain();
        }

        public async ValueTask<IReadOnlyList<IAccountModel>> GetLiveAsync(string traderId)
        {
            var id = await _ctx.HttpContext.GetBoUserId();
            var request = new GetCrmAccountsGrpcRequest
                {TraderId = traderId, BackofficeUserId = id};

            return await _dataSource.GetLiveAccountsAsync(request)
                .SelectAsync(itm => itm.ToDomain())
                .AsListAsync();
        }

        public async ValueTask<IReadOnlyList<IAccountModel>> GetDemoAsync(string traderId)
        {
            var id = await _ctx.HttpContext.GetBoUserId();
            var request = new GetCrmAccountsGrpcRequest
                {TraderId = traderId, BackofficeUserId = id};

            return await _dataSource.GetDemoAccountsAsync(request)
                .SelectAsync(itm => itm.ToDomain())
                .AsListAsync();
        }

        public async ValueTask<IDealingInfoModel> GetDealingInfoAsync(string traderId, string accountId)
        {
            var request = new DealingInfoRequest {TraderId = traderId, AccountId = accountId};
            var dealingInfo = await _dataSource.GetDealingInfoAsync(request);
            return dealingInfo.ToDomain();
        }

        public async ValueTask<string> BalanceCorrection(string traderId, string accountId, double delta, string comment)
        {
            var id = await _ctx.HttpContext.GetBoUserId();
            var request = new BalanceCorrectionGrpcRequest
                {TraderId = traderId, AccountId = accountId, Delta = delta, BoUserId = id, Comment = comment};
            return (await _dataSource.BalanceCorrectionAsync(request)).Status.ToString();
        }

        public async ValueTask TransferMoneyBetweenAccounts(string traderId, string fromAccountId, string toAccountId,
            string comment,
            double amount)
        {
            var id = await _ctx.HttpContext.GetBoUserId();
            var request = new TransferMoneyBetweenAccountsRequest
            {
                BoUserId = id, TraderId = traderId, TransferFromAccountId = fromAccountId,
                TransferToAccountId = toAccountId, Comment = comment, Amount = amount
            };

            await _dataSource.TransferMoneyBetweenAccounts(request);
        }
    }
}