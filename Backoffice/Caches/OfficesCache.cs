using System.Collections.Generic;
using System.Linq;
using Backoffice.Abstractions.Bo;
using DotNetCoreDecorators;

namespace Backoffice.Caches
{
    public static class OfficesCache
    {
        private static readonly object _lock = new();
        private static List<IOffice> _cachedItems = new();

        public static IReadOnlyList<IOffice> GetOffices()
        {
            lock (_lock)
            {
                return _cachedItems.AsReadOnlyList();
            }
        }
        
        public static IOffice GetById(string officeId)
        {
            lock (_lock)
            {
                return _cachedItems.FirstOrDefault(itm => itm.Id == officeId);
            }
        }

        public static void SyncData(IEnumerable<IOffice> src)
        {
            lock (_lock)
            {
                _cachedItems = src.ToList();
            }
        }

        public static string GetOfficeNameById(this string src)
        {
            lock (_lock)
            {
                return _cachedItems.FirstOrDefault(itm => itm.Id == src)?.Name ?? "No office";
            }
        }
    }
}