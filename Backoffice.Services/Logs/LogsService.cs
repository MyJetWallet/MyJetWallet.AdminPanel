using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Transactions;
using Backoffice.Abstractions.Models;
using Backoffice.Abstractions.Services;
using Backoffice.Services.Accounts;
using DotNetCoreDecorators;
using Microsoft.AspNetCore.Http;
using MyCRM.Logs.GrpcContracts;
using MyCRM.Logs.GrpcContracts.Contracts;

namespace Backoffice.Services.Logs
{
    public class LogsService : ILogService
    {
        private readonly IHttpContextAccessor _ctx;
        private readonly IMyCrmLogsGrpcService _dataSource;

        public LogsService(IHttpContextAccessor ctx, IMyCrmLogsGrpcService dataSource)
        {
            _ctx = ctx;
            _dataSource = dataSource;
        }

        public async IAsyncEnumerable<IRegisterLogModel> GetRegistrationLogsAsync(DateTime dateFrom,
            DateTime dateTo)
        {
            var request = new RegistrationLogGrpcRequest
                {BackOfficeUser = await _ctx.HttpContext.GetBoUserId(), DateFrom = dateFrom, DateTo = dateTo};

            var regs = _dataSource.GetRegistrationsAsync(request);
            
            await foreach (var itm in regs)
                yield return itm.ToDomain();
        }

        public async ValueTask<IReadOnlyList<IAuthLogModel>> GetAuthLogsAsync(string traderId, DateTime dateFrom,
            DateTime dateTo)
        {
            var request = new GetAuthenticationsGrpcRequest
            {
                BackOfficeUserId = await _ctx.HttpContext.GetBoUserId(), TraderId = traderId, DateFrom = dateFrom,
                DateTo = dateTo
            };

            return await _dataSource.GetAuthenticationsAsync(request)
                .SelectAsync(itm => itm.ToDomain())
                .AsListAsync();
        }
    }
}