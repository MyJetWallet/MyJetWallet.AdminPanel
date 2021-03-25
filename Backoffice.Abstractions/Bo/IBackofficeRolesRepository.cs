using System.Collections.Generic;
using System.Threading.Tasks;

namespace Backoffice.Abstractions.Bo
{
    public interface IBackofficeRolesRepository
    {
        Task AddAsync(IBackofficeRoleModel backOfficeRole);
        Task EditAsync(IBackofficeRoleModel data);
        Task<IEnumerable<IBackofficeRoleModel>> GetAllRolesAsync();
        Task<IBackofficeRoleModel> GetRoleAsync(string id);
    }
}