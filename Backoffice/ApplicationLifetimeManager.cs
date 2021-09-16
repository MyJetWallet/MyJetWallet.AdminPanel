using System;
using Backoffice.Abstractions.Bo;
using Backoffice.Abstractions.Services;
using Backoffice.Caches;
using Backoffice.GlobalTimers;
using Backoffice.Services;
using DotNetCoreDecorators;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MyJetWallet.Sdk.Service;
using MyNoSqlServer.DataReader;

namespace Backoffice
{
    public class ApplicationLifetimeManager : ApplicationLifetimeManagerBase
    {
        private readonly ILogger<ApplicationLifetimeManager> _logger;
        private readonly MyNoSqlTcpClient _noSqlTcpClient;
        private readonly IBackofficeRolesRepository _backofficeRolesRepository;
        private readonly ILoggerFactory _loggerFactory;

        private static readonly TaskTimer StatusTimer = new(TimeSpan.FromSeconds(30));

        public ApplicationLifetimeManager(
            IHostApplicationLifetime appLifetime, 
            ILogger<ApplicationLifetimeManager> logger,
            MyNoSqlTcpClient noSqlTcpClient,
            IBackofficeRolesRepository backofficeRolesRepository,
            
            IBoUsersService boUsersService,
            ILoggerFactory loggerFactory
            )
            : base(appLifetime)
        {
            _logger = logger;
            _noSqlTcpClient = noSqlTcpClient;
            _backofficeRolesRepository = backofficeRolesRepository;
            _loggerFactory = loggerFactory;

            HttpUtils.BoUsersService = boUsersService;
        }

        protected override void OnStarted()
        {
            _logger.LogInformation("OnStarted has been called.");

            _noSqlTcpClient.Start();
            
            //StatusTimer.Register("CacheSync", async () =>
            //{
                
            //});

            StatusTimer.Register("BoDataSync", async () =>
            {
                RolesCache.SyncData(await _backofficeRolesRepository.GetAllRolesAsync());
            });

            StatusTimer.Start();
            
            RefreshTimer.SetupTimer(_loggerFactory.CreateLogger("RefreshTimer"), TimeSpan.FromSeconds(5));
        }

        protected override void OnStopping()
        {
            _logger.LogInformation("OnStopping has been called.");
            
            RefreshTimer.StopTimer();
            
            _noSqlTcpClient.Stop();

            StatusTimer.Stop();
        }

        protected override void OnStopped()
        {
            _logger.LogInformation("OnStopped has been called.");
        }
    }
}