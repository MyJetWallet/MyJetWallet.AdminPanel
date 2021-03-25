using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Backoffice.Abstractions.Models;
using Backoffice.Abstractions.Services;
using DotNetCoreDecorators;
using Microsoft.AspNetCore.Http;
using MyCrm.AuditLog.Grpc;
using MyCrm.AuditLog.Grpc.Contracts;

namespace Backoffice.Services.AuditLog
{
    public class AuditLogService : IAuditLogService
    {
        private readonly IHttpContextAccessor _ctx;
        private readonly IMyCrmAuditLogGrpcService _dataSource;
        
        public AuditLogService(IHttpContextAccessor ctx, IMyCrmAuditLogGrpcService dataSource)
        {
            _ctx = ctx;
            _dataSource = dataSource;
        }
        
        public async ValueTask<IReadOnlyList<IAuditLogModel>> GetByTraderId(string traderId, DateTime from, DateTime to)
        {
            var request = new GetAuditLogByTraderIdRequest {TraderId = traderId, From = from, To = to};
            
            return await _dataSource.GetAllByTraderAsync(request).SelectAsync(itm => itm.ToDomain())
                .AsListAsync();
        }
    }
}