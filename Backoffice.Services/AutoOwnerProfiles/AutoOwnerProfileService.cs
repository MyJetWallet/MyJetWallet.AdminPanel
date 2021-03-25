using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Backoffice.Abstractions.Models;
using Backoffice.Abstractions.Services;
using DotNetCoreDecorators;
using MyCrm.AutoOwnerProfiles.Grpc;

namespace Backoffice.Services.AutoOwnerProfiles
{
    public class AutoOwnerProfileService : IAutoOwnerProfileService
    {
        private readonly IMyCrmAutoOwnerProfileGrpcServices _crmDatasource;

        public AutoOwnerProfileService(IMyCrmAutoOwnerProfileGrpcServices crmDatasource)
        {
            _crmDatasource = crmDatasource;
        }

        public async Task AddAutoOwnerProfileAsync(IAutoOwnerProfileModel src)
        {
            await _crmDatasource.AddAutoOwnerProfileAsync(src.ToGrpc());
        }

        public async Task<IEnumerable<IAutoOwnerProfileModel>> GetAutoOwnerProfilesAsync()
        {
            var result = _crmDatasource.GetAutoOwnerProfilesAsync();

            return await result.SelectAsync(itm =>
            {
                itm.SupportedCountries ??= Array.Empty<string>();
                return itm.ToDomain();
            }).AsListAsync();
        }

        public async Task<IEnumerable<IBrand>> GetBrandsAsync()
        {
            return await _crmDatasource.GetBrandsInfoAsync()
                .SelectAsync(itm => new Brand {Id = itm.Id, BaseDomain = itm.BaseDomain})
                .AsListAsync();
        }
    }
}