using System.Collections.Generic;
using System.Threading.Tasks;

namespace Backoffice.Services.SpotInstruments
{
    public interface ISpotInstrumentManager
    {
        Task<List<SpotInstrumentItem>> GetAll();
        
        Task AddInstrument(SpotInstrumentItem item);
        Task UpdateInstrument(SpotInstrumentItem item);
        Task DeleteInstrument(SpotInstrumentItem item);
        
        Task AddBrand(SpotInstrumentItem item, string brand);
        Task RemoveBrand(SpotInstrumentItem item, string brand);
    }
}