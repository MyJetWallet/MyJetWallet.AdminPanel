using System.Collections.Generic;
using System.Threading.Tasks;
using Backoffice.Abstractions.Models;
using Backoffice.Abstractions.Services;
using DotNetCoreDecorators;
using Microsoft.AspNetCore.Http;
using MyCRM.AccountTransactions.Grpc;
using MyCRM.AccountTransactions.Grpc.Contracts;

namespace Backoffice.Services.Transactions
{
    public class TransactionService : ITransactionsService
    {
        private readonly IHttpContextAccessor _ctx;
        private readonly IMyCrmAccountTransactionsGrpcService _dataSource;

        public TransactionService(IHttpContextAccessor ctx, IMyCrmAccountTransactionsGrpcService dataSource)
        {
            _ctx = ctx;
            _dataSource = dataSource;
        }

        public async ValueTask<IReadOnlyList<IBoTransactionModel>> GetAsync(string traderId, string accountId)
        {
            var request = new GetAccountTransactionsGrpcRequest
                {BackOfficeUserId = await _ctx.HttpContext.GetBoUserId(), TraderId = traderId, AccountId = accountId};

            return await _dataSource.GetAsync(request)
                .SelectAsync(itm => itm.ToDomain()).AsListAsync();
        }
    }
}