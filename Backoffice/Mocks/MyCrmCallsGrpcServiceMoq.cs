using System.Threading.Tasks;
using MyCrm.Calls.Grpc;
using MyCrm.Calls.Grpc.Contracts;

namespace Backoffice.Mocks
{
    public class MyCrmCallsGrpcServiceMoq: IMyCrmCallsGrpcService
    {
        public ValueTask<CallGrpcResponse> CallAsync(CallGrpcRequest request)
        {
            throw new System.NotImplementedException();
        }

        public ValueTask<DownloadCallGrpcResponse> DownloadCall(DownloadCallGrpcRequest request)
        {
            throw new System.NotImplementedException();
        }
    }
}