using System.Threading;
using System.Threading.Tasks;
using MyCrm.TraderMarketingSalesData.Grpc.Backoffice;
using MyCrm.TraderMarketingSalesData.Grpc.Backoffice.Contracts.Requests;

namespace Backoffice.Mocks
{
    public class MyCrmWriterTraderMarketingSalesDataForBackofficeGrpcServiceMoq: IMyCrmWriterTraderMarketingSalesDataForBackofficeGrpcService
    {
        public ValueTask UpsertAchievementCRMStatusAsync(UpsertAchievementCRMStatusGrpcRequest request,
            CancellationToken cancellationToken = new CancellationToken())
        {
            throw new System.NotImplementedException();
        }
    }
}