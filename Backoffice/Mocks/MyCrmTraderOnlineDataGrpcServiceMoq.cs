using System.Threading.Tasks;
using MyCrm.TraderOnlineData.Grpc;
using MyCrm.TraderOnlineData.Grpc.Requests;
using MyCrm.TraderOnlineData.Grpc.Responses;

namespace Backoffice.Mocks
{
    public class MyCrmTraderOnlineDataGrpcServiceMoq: IMyCrmTraderOnlineDataGrpcService
    {
        public ValueTask<TraderOnlineDataGrpcResponse> GetTraderOnlineDataAsync(TraderOnlineDataGrpcRequest request)
        {
            throw new System.NotImplementedException();
        }
    }
}