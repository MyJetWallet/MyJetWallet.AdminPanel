using System.Collections.Generic;
using System.Threading.Tasks;
using Service.AssetsDictionary.Domain.Models;

namespace Backoffice.Services.SpotExternalInstruments
{
    public interface ISpotExternalInstrumentManager
    {
        Task<List<SpotConvertExternalInstrument>> GetAll();
        
        Task AddInstrument(SpotConvertExternalInstrument item);
        Task UpdateInstrument(SpotConvertExternalInstrument item);
        Task DeleteInstrument(SpotConvertExternalInstrument item);
    }
}