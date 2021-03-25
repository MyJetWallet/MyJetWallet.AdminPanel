using System.Collections.Generic;
using System.Linq;
using Backoffice.Abstractions.Bo;
using DotNetCoreDecorators;

namespace Backoffice.Caches
{
    public static class RolesCache
    {
        private  static readonly object _lock = new();
        private  static Dictionary<string, IBackofficeRoleModel> _cachedItems = new ();

        public static IReadOnlyList<IBackofficeRoleModel> GetRoles()
        {
            lock (_lock)
            {
                return _cachedItems.Values.AsReadOnlyList();
            }
        }
        
        public static Dictionary<string, IBackofficeRoleModel> GetRolesAsDictionary()
        {
            lock (_lock)
            {
                return _cachedItems;
            }
        }

        public static IBackofficeRoleModel GetRole(string id)
        {
            lock (_lock)
            {
                if (_cachedItems.ContainsKey(id))
                {
                    return _cachedItems[id];
                }
            }

            return null;
        }

        public static void SyncData(IEnumerable<IBackofficeRoleModel> roles)
        {
            lock (_lock)
            {
                _cachedItems = roles.ToDictionary(itm => itm.Id);
            }
        }
    }
}