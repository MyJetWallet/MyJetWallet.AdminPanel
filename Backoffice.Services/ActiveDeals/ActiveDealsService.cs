using System.Collections.Generic;
using System.Threading.Tasks;
using Backoffice.Abstractions.Models;
using Backoffice.Abstractions.Services;
using DotNetCoreDecorators;
using Microsoft.AspNetCore.Http;
using MyCRM.AccountTransactions.Grpc;
using MyCRM.AccountTransactions.Grpc.Contracts;

namespace Backoffice.Services.ActiveDeals
{
    public class ActiveDealsService : IActiveDealService
    {
        private readonly IHttpContextAccessor _ctx;
        private readonly IMyCrmActiveDealsGrpcService _dataSource;

        public ActiveDealsService(IHttpContextAccessor ctx, IMyCrmActiveDealsGrpcService dataSource)
        {
            _ctx = ctx;
            _dataSource = dataSource;
        }

        public async Task<IReadOnlyList<IActiveDealModel>> GetByTraderIdAsync(string traderId)
        {
            var id = await _ctx.HttpContext.GetBoUserId();
            var request = new GetActiveDealsGrpcRequest
                {TraderId = traderId, BackOfficeUserId = id};

            return await _dataSource.GetAsync(request)
                .SelectAsync(itm => itm.ToDomain())
                .AsListAsync();
        }
    }
}