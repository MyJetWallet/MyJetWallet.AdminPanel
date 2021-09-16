using System.Collections.Generic;
using System.Threading.Tasks;

namespace Backoffice.Abstractions.Bo
{
    public interface IBackofficeRolesRepository
    {
        Task AddAsync(IBackofficeRoleModel backOfficeRole);
        Task EditAsync(IBackofficeRoleModel data);
        Task DeleteAsync(IBackofficeRoleModel data);
        Task<IReadOnlyList<IBackofficeRoleModel>> GetAllRolesAsync();
        Task<IBackofficeRoleModel> GetRoleAsync(string id);
    }
}