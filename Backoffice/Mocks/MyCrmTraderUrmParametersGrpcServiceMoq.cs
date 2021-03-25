using System.Collections.Generic;
using System.Threading.Tasks;
using MyCrm.MyCrmTradersUtmParametersGrpcContracts;
using MyCrm.MyCrmTradersUtmParametersGrpcContracts.Contracts;
using MyCrm.MyCrmTradersUtmParametersGrpcContracts.Model;

namespace Backoffice.Mocks
{
    public class MyCrmTraderUrmParametersGrpcServiceMoq: IMyCrmTraderUrmParametersGrpcService
    {
        public IAsyncEnumerable<TraderUtmParametersGrpcModel> GetTraderUtmParametersAsync(GetTraderUrmParametersGrpcRequest request)
        {
            throw new System.NotImplementedException();
        }

        public ValueTask UpdateTraderUtmsAsync(UpdateTraderUtmsGrpcRequest request)
        {
            throw new System.NotImplementedException();
        }
    }
}