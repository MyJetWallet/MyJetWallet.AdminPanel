using System.Collections.Generic;
using System.Threading.Tasks;
using Service.Fees.Domain.Models;

namespace Backoffice.Services.Fees.Settings
{
    public interface IFeesSettingsManager
    {
        Task<List<FeesSettings>> GetAll();
        Task CreateFeesSettings(FeesSettings item);
        Task UpdateFeesSettings(FeesSettings item);
        Task DeleteFeesSettings(FeesSettings item);
    }
}