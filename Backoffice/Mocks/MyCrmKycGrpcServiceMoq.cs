using System.Collections.Generic;
using System.Threading.Tasks;
using MyCrm.Kyc.Grpc;
using MyCrm.Kyc.Grpc.Contracts;
using MyCrm.Kyc.Grpc.Models;

namespace Backoffice.Mocks
{
    public class MyCrmKycGrpcServiceMoq: IMyCrmKycGrpcService
    {
        public ValueTask<TradersToCheckKycGrpcResponse> GetTradersToCheckKycAsync(GetTradersToCheckKycGrpcRequest request)
        {
            throw new System.NotImplementedException();
        }

        public ValueTask<WaitingForReviewAmountGrpcResponse> GetWaitingForReviewAmountAsync(GetWaitingForReviewAmountGrpcRequest request)
        {
            throw new System.NotImplementedException();
        }

        public IAsyncEnumerable<MyCrmTraderToCheckKycModel> GetTradersByDateTimeAsync(GetTradersByDateTimeGrpcRequest request)
        {
            throw new System.NotImplementedException();
        }

        public ValueTask UpdateKycStatusGrpcRequest(UpdateKycStatusGrpcRequest request)
        {
            throw new System.NotImplementedException();
        }

        public ValueTask<GetKeyStatusGrpcResponse> GetKycStatusAsync(GetKycStatusGrpcRequest request)
        {
            throw new System.NotImplementedException();
        }
    }
}