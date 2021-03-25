using System.Threading.Tasks;
using MyCrm.AffiliateAccess.Grpc;
using MyCrm.AffiliateAccess.Grpc.Models;

namespace Backoffice.Mocks
{
    public class MyCrmAffiliateAccessServiceGrpcMoq: IMyCrmAffiliateAccessServiceGrpc
    {
        public ValueTask<AccessGrpcResponse> GetAccessAsync()
        {
            throw new System.NotImplementedException();
        }

        public ValueTask<AccessGrpcResponse> SaveAccessAsync(AccessGrpcModel request)
        {
            throw new System.NotImplementedException();
        }

        public ValueTask DeleteAsync(DeleteGrpcModel request)
        {
            throw new System.NotImplementedException();
        }
    }
}