using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backoffice.Abstractions.Services;
using DotNetCoreDecorators;
using Microsoft.AspNetCore.Http;
using MyCrm.TraderCrmStatuses.Grpc;
using MyCrm.TraderCrmStatuses.Grpc.Contracts;
using MyCrm.TraderMarketingSalesData.Grpc.Backoffice;
using MyCrm.TraderMarketingSalesData.Grpc.Backoffice.Contracts.Requests;

namespace Backoffice.Services.Status
{
    public class StatusService : IStatusService
    {
        private readonly IHttpContextAccessor _ctx;
        private readonly IMyCrmTraderCrmStatusesGrpcService _dataSource;
        private readonly IMyCrmReaderTraderMarketingSalesDataForBackofficeGrpcService _dataSourceAchievementsReader;
        private readonly IMyCrmWriterTraderMarketingSalesDataForBackofficeGrpcService _dataSourceAchievementsWriter;
        
        public StatusService(IHttpContextAccessor ctx, IMyCrmTraderCrmStatusesGrpcService dataSource,
            IMyCrmWriterTraderMarketingSalesDataForBackofficeGrpcService writer,
            IMyCrmReaderTraderMarketingSalesDataForBackofficeGrpcService reader)
        {
            _ctx = ctx;
            _dataSource = dataSource;
            _dataSourceAchievementsReader = reader;
            _dataSourceAchievementsWriter = writer;
        }
        
        public async Task<IEnumerable<IBoTraderStatus>> GetCrmStatusesAsync()
        {
            var request = new GetStatusesGrpcRequest {BackOfficeId = "admin"};
            
            return await _dataSource.GetStatusesAsync(request)
                .SelectAsync(itm => itm.ToDomain())
                .AsListAsync();
        }

        public async Task<IEnumerable<IBoTraderStatus>> GetTradingStatusesAsync()
        {
            var request = new GetStatusesGrpcRequest {BackOfficeId = "admin"};
            
            return await _dataSource.GetStatusesAsync(request)
                .SelectAsync(itm => itm.ToDomain())
                .AsListAsync();
        }

        public async Task<IEnumerable<IBoAchievementsStatus>> GetAchievementsStatusesAsync()
        {
            return (await _dataSourceAchievementsReader.GetAchievementStatusesAsync(new GetAchievementStatusesGrpsRequest()))
                .AchievementStatuses.Select(itm => itm.ToDomain());
        }

        public async ValueTask UpdateCrmStatusAsync(string traderId, string statusId)
        {
            var request = new UpdateTraderCrmStatusRequest
                {BackOfficeUserId = await _ctx.HttpContext.GetBoUserId(), CrmStatus = statusId, TraderId = traderId};
            
            await _dataSource.UpdateStatusAsync(request);
        }

        public async ValueTask UpdateTradingStatusAsync(string traderId, string statusId)
        {
            var request = new UpdateTraderTradingStatusRequest
                {BackOfficeUserId = await _ctx.HttpContext.GetBoUserId(), TradingStatus = statusId, TraderId = traderId};
            
            await _dataSource.UpdateTradingStatus(request);
        }

        public async ValueTask UpdateAchievementsStatusAsync(string traderId, string statusId)
        {
            var request = new UpsertAchievementCRMStatusGrpcRequest
                {TraderId = traderId, BackOfficeUserId = await _ctx.HttpContext.GetBoUserId(), AchievementCRMStatus = statusId};
            
            await _dataSourceAchievementsWriter.UpsertAchievementCRMStatusAsync(request);
        }
    }
}