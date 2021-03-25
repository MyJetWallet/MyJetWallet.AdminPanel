using System.Threading;
using System.Threading.Tasks;
using MyCrm.TraderMarketingSalesData.Grpc.Backoffice;
using MyCrm.TraderMarketingSalesData.Grpc.Backoffice.Contracts.Requests;
using MyCrm.TraderMarketingSalesData.Grpc.Backoffice.Contracts.Responses;
using MyCrm.TraderMarketingSalesData.Grpc.Backoffice.Contracts.Responses.Models;

namespace Backoffice.Mocks
{
    public class MyCrmReaderTraderMarketingSalesDataForBackofficeGrpcServiceMoq: IMyCrmReaderTraderMarketingSalesDataForBackofficeGrpcService
    {
        public ValueTask<TraderMarketingSalesDataGrpcResponse> GetLiveAsync(GetTraderMarketingSalesDataGrpcRequest request,
            CancellationToken cancellationToken = new CancellationToken())
        {
            throw new System.NotImplementedException();
        }

        public async ValueTask<AchievementStatusesGrpsResponse> GetAchievementStatusesAsync(GetAchievementStatusesGrpsRequest request,
            CancellationToken cancellationToken = new CancellationToken())
        {
            return new AchievementStatusesGrpsResponse()
            {
                AchievementStatuses = new []
                {
                    new AchievementStatusGrpsModel()
                    {
                        AchievementStatus = "AchievementStatus"
                    }
                }
            };

        }
    }
}