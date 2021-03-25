using System.Collections.Generic;
using System.Threading.Tasks;
using Backoffice.Abstractions.Models;

namespace Backoffice.Abstractions.Services
{
    public interface IAffiliateService
    {
        Task<IReadOnlyList<IAffiliate>> GetAsync();
        Task AddOrUpdate(IAffiliate src);
        Task DeleteAsync(string apiKey);
    }
}