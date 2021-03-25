using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Backoffice.Abstractions.Bo;
using Backoffice.Caches;

namespace Backoffice
{
    public static class UsersUtils
    {
        private static readonly IEnumerable<string> _techFields = new List<string>
        {
            "IsBlocked"
        };

        public static string GetOnlineOfflineUserIcon(this IBackOfficeUser src)
        {
            return OnlineCache.IsOnline(src.Id) ? "images/online.png" : "images/offline.png";
        }

        public static string GetLastLoginIp(this IBackOfficeUser src)
        {
            return "127.0.0.1";
        }

        public static bool HasRight(this IBackOfficeUser user, string userRight)
        {
            if (user == null)
                return false;

            return user.IsAdmin || user.HasRight(RolesCache.GetRolesAsDictionary(), userRight);
        }

        private static bool HasRight(this IBackOfficeUser user,
            IReadOnlyDictionary<string, IBackofficeRoleModel> allRoles, string rightName)
        {
            if (user.IsAdmin)
                return true;

            return user.Roles.Where(allRoles.ContainsKey)
                .Select(roleId => allRoles[roleId])
                .Any(role => role.Rights.Any(roleRight => roleRight == rightName));
        }

        public static IEnumerable<string> GetUserPermissionsAsString(this IBackOfficeUser src)
        {
            return src.GetType().GetProperties()
                .Where(itm =>
                    itm.PropertyType == typeof(bool) && !_techFields.Contains(itm.Name) && (bool) itm.GetValue(src))
                .Select(itm => Regex.Replace(itm.Name, "(\\B[A-Z])", " $1"));
        }
    }
}