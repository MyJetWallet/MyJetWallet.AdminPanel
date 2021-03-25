using System.Collections.Generic;
using Backoffice.Abstractions.Services;
using DotNetCoreDecorators;

namespace Backoffice.Caches
{
    public static class StatusCaches
    {
        private static readonly object _lock = new();
        private static readonly object _achievementsLock = new();

        private static IEnumerable<IBoTraderStatus> _tradingStatuses = new List<IBoTraderStatus>();
        private static IEnumerable<IBoTraderStatus> _crmStatuses = new List<IBoTraderStatus>();
        private static IEnumerable<IBoAchievementsStatus> _achievementsStatuses = new List<IBoAchievementsStatus>();
        
        public static IReadOnlyList<IBoTraderStatus> GetTradingStatuses()
        {
            lock (_lock)
            {
                return _tradingStatuses.AsReadOnlyList();
            }
        }

        public static IReadOnlyList<IBoTraderStatus> GetCrmStatuses()
        {
            lock (_lock)
            {
                return _crmStatuses.AsReadOnlyList();
            }
        }

        public static IReadOnlyList<IBoAchievementsStatus> GetAchievementsStatuses()
        {
            lock (_achievementsLock)
            {
                return _achievementsStatuses.AsReadOnlyList();
            }
        }

        public static void SyncData(IEnumerable<IBoTraderStatus> tradingStatuses, IEnumerable<IBoTraderStatus> crmStatuses,
            IEnumerable<IBoAchievementsStatus> achievementStatuses)
        {
            lock (_lock)
            {
                _tradingStatuses = tradingStatuses;
                _crmStatuses = crmStatuses;
            }

            lock (_achievementsLock)
            {
                _achievementsStatuses = achievementStatuses;
            }
        }
    }
}