using System.Collections.Generic;
using System.Threading.Tasks;
using Backoffice.Abstractions.Models;

namespace Backoffice.Abstractions.Services
{
    public interface IAutoOwnerProfileService
    {
        Task AddAutoOwnerProfileAsync(IAutoOwnerProfileModel src);
        Task<IEnumerable<IAutoOwnerProfileModel>> GetAutoOwnerProfilesAsync();
        Task<IEnumerable<IBrand>> GetBrandsAsync();
    }
}