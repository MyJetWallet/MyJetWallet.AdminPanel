using System.Linq;
using Backoffice.Abstractions.Bo;
using Backoffice.Abstractions.Models;

namespace Backoffice.RenderUtils
{
    public static class IsShowUser
    {
        public static bool IsRenderUser(this IBackOfficeUser user, IRegisterLogModel src)
        {
            if (user.IsAdmin)
                return true;
            
            if (src.Owner != null && user.DataOwnership != null && user.DataOwnerships.Any() &&
                (user.DataOwnership == src.Owner || user.DataOwnerships.Contains(src.Owner)))
            {
                return true;
            }

            if (src.AssignedBackOfficeUserId == user.Id)
            {
                return true;
            }

            return src.RetentionManagerId == user.Id;
        }
    }
}