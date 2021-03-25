using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backoffice.Abstractions.Models;
using Backoffice.Abstractions.Services;
using DotNetCoreDecorators;
using Microsoft.AspNetCore.Http;
using MyCrm.Kyc.Grpc;
using MyCrm.Kyc.Grpc.Contracts;

namespace Backoffice.Services.KYC
{
    public class TradersKycService : ITradersKycService
    {
        private readonly IHttpContextAccessor _ctx;
        private readonly IMyCrmKycGrpcService _dataSource;
        
        public TradersKycService(IHttpContextAccessor ctx, IMyCrmKycGrpcService dataSource)
        {
            _ctx = ctx;
            _dataSource = dataSource;
        }
        
        public async ValueTask UpdateStateAsync(string userAgent, string ip, string traderId, BoKycState kycState)
        {
            var request = new UpdateKycStatusGrpcRequest
            {
                BackOfficeUserId = await _ctx.HttpContext.GetBoUserId(),
                TraderId = traderId,
                UserAgent = userAgent,
                Ip = ip,
                NewStatus = kycState.ToGrpcKycStatus()
            };

            await _dataSource.UpdateKycStatusGrpcRequest(request);
        }

        public async ValueTask<IReadOnlyList<ITraderToCheckKyc>> GetTradersAsync(DateTime fromDate, DateTime toDate)
        {
            var request = new GetTradersByDateTimeGrpcRequest
                {BackOfficeUserId = await _ctx.HttpContext.GetBoUserId(), DateFrom = fromDate, DateTo = toDate};

            return await _dataSource.GetTradersByDateTimeAsync(request)
                .SelectAsync(itm => itm.FromGrpcToDomain())
                .AsListAsync();
        }

        public async ValueTask<IReadOnlyList<ITraderToCheckKyc>> GetTradersToCheckAsync()
        {
            var request = new GetTradersToCheckKycGrpcRequest {BackOfficeUserId = await _ctx.HttpContext.GetBoUserId()};
            var response = await _dataSource.GetTradersToCheckKycAsync(request);

            return response.TradersToCheck?
                .Select(itm => itm.FromGrpcToDomain())
                .OrderBy(itm => !itm.MissingDocuments ? 1 : 0)
                .ToList() ?? Array.Empty<ITraderToCheckKyc>().ToList();
        }

        public async ValueTask<int> GetWaitingForReviewCountAsync()
        {
            var request  = new GetWaitingForReviewAmountGrpcRequest {BackOfficeUserId = await _ctx.HttpContext.GetBoUserId()};
            return (await _dataSource.GetWaitingForReviewAmountAsync(request)).Amount;
        }

        public async ValueTask<BoKycState> GetKycStateAsync(string traderId)
        {
            var request = new GetKycStatusGrpcRequest
                {TraderId = traderId, BackOfficeUserId = await _ctx.HttpContext.GetBoUserId()};
        
            return (await _dataSource.GetKycStatusAsync(request)).KycStatus.GrpcToDomainKycStatus();
        }
    }
}