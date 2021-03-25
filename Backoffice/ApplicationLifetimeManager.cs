using System;
using Backoffice.Abstractions.Bo;
using Backoffice.Abstractions.Services;
using Backoffice.Caches;
using Backoffice.Services;
using DotNetCoreDecorators;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MyJetWallet.Sdk.Service;

namespace Backoffice
{
    public class ApplicationLifetimeManager : ApplicationLifetimeManagerBase
    {
        private readonly ILogger<ApplicationLifetimeManager> _logger;
        private readonly IStatusService _statusService;
        private readonly IBackofficeRolesRepository _backofficeRolesRepository;
        private readonly IBackofficeOfficeService _backofficeOfficeService;

        private static readonly TaskTimer StatusTimer = new(TimeSpan.FromSeconds(30));

        public ApplicationLifetimeManager(IHostApplicationLifetime appLifetime, ILogger<ApplicationLifetimeManager> logger,
            IStatusService statusService,
            IBackofficeRolesRepository backofficeRolesRepository,
            IBackofficeOfficeService backofficeOfficeService,
            IBoUsersService boUsersService
            )
            : base(appLifetime)
        {
            _logger = logger;
            _statusService = statusService;
            _backofficeRolesRepository = backofficeRolesRepository;
            _backofficeOfficeService = backofficeOfficeService;

            HttpUtils.BoUsersService = boUsersService;
        }

        protected override void OnStarted()
        {
            _logger.LogInformation("OnStarted has been called.");

            StatusTimer.Register("CacheSync", async () =>
            {
                var crmStatuses = await _statusService.GetTradingStatusesAsync();
                var tradingStatuses = await _statusService.GetCrmStatusesAsync();
                var achievementsStatuses = await _statusService.GetAchievementsStatusesAsync();
                StatusCaches.SyncData(tradingStatuses, crmStatuses, achievementsStatuses);
            });

            StatusTimer.Register("BoDataSync", async () =>
            {
                OfficesCache.SyncData(await _backofficeOfficeService.GetAsync());
                RolesCache.SyncData(await _backofficeRolesRepository.GetAllRolesAsync());
            });

            StatusTimer.Start();
        }

        protected override void OnStopping()
        {
            _logger.LogInformation("OnStopping has been called.");

            StatusTimer.Stop();
        }

        protected override void OnStopped()
        {
            _logger.LogInformation("OnStopped has been called.");
        }
    }
}