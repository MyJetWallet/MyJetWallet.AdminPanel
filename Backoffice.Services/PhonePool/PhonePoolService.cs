using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backoffice.Abstractions.Models;
using Backoffice.Abstractions.Services;
using DotNetCoreDecorators;
using Microsoft.AspNetCore.Http;
using MyCrm.Calls.Grpc;
using MyCrm.Calls.Grpc.Contracts;
using MyCrm.Calls.Grpc.Models;

namespace Backoffice.Services.PhonePool
{
    public class PhonePoolService : IPhonePoolService
    {
        private readonly IHttpContextAccessor _ctx;
        private readonly IMyCrmPhonePoolGrpcService _dataSource;
        private readonly IMyCrmCallsGrpcService _callsService;

        public PhonePoolService(IHttpContextAccessor ctx, IMyCrmPhonePoolGrpcService dataSource,
            IMyCrmCallsGrpcService calls)
        {
            _ctx = ctx;
            _dataSource = dataSource;
            _callsService = calls;
        }

        public async ValueTask<IReadOnlyList<IPhonePoolModel>> GetAsync()
        {
            var request = new GetPhonesPoolsGrpcRequest {BackOfficeUserId = await _ctx.HttpContext.GetBoUserId()};

            return await _dataSource.GetPhonePoolsAsync(request)
                .SelectAsync(itm => itm.ToDomain())
                .AsListAsync();
        }

        public async ValueTask<IPhonePoolModel> GetAsync(string id)
        {
            var request = new GetPhonesPoolsGrpcRequest {BackOfficeUserId = "admin"};

            var result = await _dataSource.GetPhonePoolsAsync(request)
                .FirstOrDefaultAsync(itm => itm.Id == id);

            return result.ToDomain();
        }

        public async Task DeleteAsync(string id)
        {
            var request = new DeletePhonePoolGrpcRequest
                {BackOfficeUserId = await _ctx.HttpContext.GetBoUserId(), PoolId = id};
            await _dataSource.DeletePhonePoolAsync(request);
        }

        public async ValueTask InsertOrReplaceAsync(string id, string name,
            IEnumerable<(string name, string number)> numbers)
        {
            await _dataSource.InsertOrUpdateAsync(new PhonePoolGrpcModel
            {
                Id = id ?? Guid.NewGuid().ToString("N"),
                Name = name,
                Numbers = numbers.Select(itm => new PhoneNumberGrpcModel
                {
                    Id = Guid.NewGuid().ToString("N"),
                    Name = itm.name,
                    Number = itm.number
                }).ToArray()
            });
        }

        public async ValueTask<BoCallResponseEnum> CallAsync(ICallModel callModel)
        {
            var result = await _callsService
                .CallAsync(callModel.ToGrpcCallRequest(await _ctx.HttpContext.GetBoUserId()));
            return result.Status.ToDomain();
        }
    }
}