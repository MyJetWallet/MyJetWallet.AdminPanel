using System.Collections.Generic;
using System.Threading.Tasks;
using Service.External.B2C2.Domain.Models.Settings;

namespace Backoffice.Services.ExternalMarkets.B2C2
{
    public interface IExternalMarketsB2C2Manager
    {
        Task<List<ExternalMarketSettings>> GetAll();
        Task CreateExternalMarketSettings(ExternalMarketSettings item);
        Task UpdateExternalMarketSettings(ExternalMarketSettings item);
        Task DeleteExternalMarketSettings(ExternalMarketSettings item);
    }
}