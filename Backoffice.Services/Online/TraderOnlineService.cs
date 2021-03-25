using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backoffice.Abstractions.Models;
using Backoffice.Abstractions.Services;
using Backoffice.Datatypes;
using MyCrm.TraderOnlineData.Grpc;
using MyCrm.TraderOnlineData.Grpc.Requests;

namespace Backoffice.Services.Online
{
    public class TraderOnlineService : ITradersOnlineService
    {
        private const int PageSize = 100;
        private readonly IMyCrmTraderOnlineDataGrpcService _datasourceService;

        public TraderOnlineService(IMyCrmTraderOnlineDataGrpcService datasourceService)
        {
            _datasourceService = datasourceService;
        }

        public async ValueTask<IEnumerable<ITraderOnline>> GetAsync(int page)
        {
            var grpcRequest = new TraderOnlineDataGrpcRequest
            {
                Page = page,
                Take = PageSize
            };

            var onlineResponse = await _datasourceService.GetTraderOnlineDataAsync(grpcRequest);
            return onlineResponse.TradersOnlineData?.Select(itm => itm.FromGrpcToDomain())
                .ToList() ?? new List<ITraderOnline>();
        }
    }
}