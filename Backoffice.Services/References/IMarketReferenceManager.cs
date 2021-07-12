using System.Collections.Generic;
using System.Threading.Tasks;

namespace Backoffice.Services.References
{
    public interface IMarketReferenceManager
    {
        Task<List<MarketReferenceItem>> GetAll();
        
        Task AddReference(MarketReferenceItem item);
        Task UpdateReference(MarketReferenceItem item);
        Task DeleteReference(MarketReferenceItem item);
        
        Task AddBrand(MarketReferenceItem item, string brand);
        Task RemoveBrand(MarketReferenceItem item, string brand);
    }
}