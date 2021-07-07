using System.Collections.Generic;
using System.Linq;
using Backoffice.Services.Simulations;
using Microsoft.Extensions.Logging;
using MyJetWallet.Sdk.ExternalMarketsSettings.Autofac;
using Service.Simulation.Binance.Client;
using Service.Simulation.FTX.Client;

namespace Backoffice.Services.ExternalMarkets
{
    public interface IExternalMarketsSettingsManager
    {
        List<ExternalMarketsSettingsService> GetExternalMarketsSettingsServices();
    }

    public class ExternalMarketsSettingsManager : IExternalMarketsSettingsManager
    {
        private List<ExternalMarketsSettingsService> _services { get; set; }
        
        private readonly ILogger<ExternalMarketsSettingsManager> _logger;

        public ExternalMarketsSettingsManager(Dictionary<string, string> settings)
        {
            _services = new List<ExternalMarketsSettingsService>();
            foreach (var setting in settings)
            {
                var clientFactory = new ExternalMarketSettingsClientFactory(setting.Value);
                var service = new ExternalMarketsSettingsService(
                    setting.Key,
                    clientFactory.GetMarketMakerSettingsManagerGrpc());

                _services.Add(service);
            }
        }

        public List<ExternalMarketsSettingsService> GetExternalMarketsSettingsServices()
        {
            return _services.ToList();
        }
    }
}