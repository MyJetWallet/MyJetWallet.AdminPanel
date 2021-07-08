using System.Collections.Generic;
using System.Threading.Tasks;
using Service.Fees.Domain.Models;

namespace Backoffice.Services.Fees.Instruments
{
    public interface IInstrumentFeesManager
    {
        Task<List<SpotInstrumentFees>> GetAll();
        Task CreateSpotInstrumentFeesSettings(SpotInstrumentFees item);
        Task UpdateSpotInstrumentFeesSettings(SpotInstrumentFees item);
        Task DeleteSpotInstrumentFeesSettings(SpotInstrumentFees item);
    }
}