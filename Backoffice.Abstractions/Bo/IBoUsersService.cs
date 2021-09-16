using System.Collections.Generic;
using System.Threading.Tasks;

namespace Backoffice.Abstractions.Bo
{
    public interface IBoUsersService
    {
        ValueTask<IEnumerable<IBackOfficeUser>> GetBoUsersAsync();
        ValueTask<IBackOfficeUser> GetBoUserById(string boUserId);
        ValueTask<IBackOfficeUser> UpdateBoUser(IBackOfficeUser boUser);
    }
}