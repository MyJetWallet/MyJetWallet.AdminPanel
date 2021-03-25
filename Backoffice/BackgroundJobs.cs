using System;
using Backoffice.Abstractions.Bo;
using Backoffice.Abstractions.Services;
using Backoffice.Caches;
using DotNetCoreDecorators;

namespace Backoffice
{
    public static class BackgroundJobs
    {
        private static readonly TaskTimer StatusTimer = new (TimeSpan.FromSeconds(30));

        public static void Init(IServiceProvider sp)
        {
            var statusService = sp.GetService<IStatusService>();
            var rolesRepository = sp.GetService<IBackofficeRolesRepository>();
            var officeService = sp.GetService<IBackofficeOfficeService>();
            
            
            StatusTimer.Register("CacheSync", async () =>
            {
                var crmStatuses = await statusService.GetTradingStatusesAsync();
                var tradingStatuses = await statusService.GetCrmStatusesAsync();
                var achievementsStatuses = await statusService.GetAchievementsStatusesAsync();
                StatusCaches.SyncData(tradingStatuses, crmStatuses, achievementsStatuses);
            });
            
            StatusTimer.Register("BoDataSync", async () =>
            {
                OfficesCache.SyncData(await officeService.GetAsync());
                RolesCache.SyncData(await rolesRepository.GetAllRolesAsync());
            });
        }

        public static void Start()
        {
            StatusTimer.Start();
        }

        private static T GetService<T>(this IServiceProvider sp)
        {
            var type = typeof(T);
            var service = (T) sp.GetService(type);

            if (service == null)
                throw new Exception($"{type.Name} not found in Service Provider");

            return service;
        }
    }
}