using System.Collections.Generic;
using System.Threading.Tasks;
using MyCrm.TraderCrmStatuses.Grpc;
using MyCrm.TraderCrmStatuses.Grpc.Contracts;
using MyCrm.TraderCrmStatuses.Grpc.Models;

namespace Backoffice.Mocks
{
    public class MyCrmTraderCrmStatusesGrpcServiceMoq: IMyCrmTraderCrmStatusesGrpcService
    {
        public async IAsyncEnumerable<CrmStatusGrpcModel> GetStatusesAsync(GetStatusesGrpcRequest request)
        {
            yield return new CrmStatusGrpcModel()
            {
                Id = request.BackOfficeId,
                Name = request.BackOfficeId
            };
        }

        public IAsyncEnumerable<CrmStatusGrpcModel> GetTradingStatuses(GetTradingStatusesContracts request)
        {
            throw new System.NotImplementedException();
        }

        public ValueTask UpdateStatusAsync(UpdateTraderCrmStatusRequest request)
        {
            throw new System.NotImplementedException();
        }

        public ValueTask UpdateTradingStatus(UpdateTraderTradingStatusRequest request)
        {
            throw new System.NotImplementedException();
        }
    }
}