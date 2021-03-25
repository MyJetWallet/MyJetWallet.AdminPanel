using System.Collections.Generic;
using System.Threading.Tasks;
using Backoffice.Abstractions.Bo;
using DotNetCoreDecorators;
using Microsoft.AspNetCore.Http;
using MyCrm.BusinessCategories.Grpc;
using MyCrm.BusinessCategories.Grpc.Contracts;

namespace Backoffice.Services.Backoffice
{
    public class BackofficeOfficeService : IBackofficeOfficeService
    {
        private readonly IMyCrmBusinessCategoriesGrpcService _crmDatasource;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public BackofficeOfficeService(IMyCrmBusinessCategoriesGrpcService crmDatasource,
            IHttpContextAccessor httpContextAccessor)
        {
            _crmDatasource = crmDatasource;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IEnumerable<IOffice>> GetAsync()
        {
            var request = new GetBusinessCategoriesGrpcRequest {BackOfficeUserId = "admin"};
            return await _crmDatasource.GetAsync(request)
                .SelectAsync(itm => itm.ToDomain())
                .AsListAsync();
        }

        public async ValueTask InsertOrReplaceAsync(IOffice model)
        {
            await _crmDatasource.InsertOrReplaceAsync(model.ToGrpc());
        }
    }
}