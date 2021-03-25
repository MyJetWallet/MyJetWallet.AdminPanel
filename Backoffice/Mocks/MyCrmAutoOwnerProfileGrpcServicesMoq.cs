using System.Collections.Generic;
using System.Threading.Tasks;
using MyCrm.AutoOwnerProfiles.Grpc;
using MyCrm.AutoOwnerProfiles.Grpc.Models;

namespace Backoffice.Mocks
{
    public class MyCrmAutoOwnerProfileGrpcServicesMoq: IMyCrmAutoOwnerProfileGrpcServices
    {
        public Task AddAutoOwnerProfileAsync(AutoOwnerProfileGrpcModel profile)
        {
            throw new System.NotImplementedException();
        }

        public IAsyncEnumerable<AutoOwnerProfileGrpcModel> GetAutoOwnerProfilesAsync()
        {
            throw new System.NotImplementedException();
        }

        public IAsyncEnumerable<BrandGrpcModel> GetBrandsInfoAsync()
        {
            throw new System.NotImplementedException();
        }
    }
}