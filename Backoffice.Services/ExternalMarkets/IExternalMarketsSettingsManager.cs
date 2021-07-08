using System.Collections.Generic;
using System.Linq;
using MyJetWallet.Sdk.ExternalMarketsSettings.Autofac;

namespace Backoffice.Services.ExternalMarkets
{
    public interface IExternalMarketsSettingsManager
    {
        List<ExternalMarketsSettingsService> GetExternalMarketsSettingsServices();
    }

    public class ExternalMarketsSettingsManager : IExternalMarketsSettingsManager
    {
        private List<ExternalMarketsSettingsService> _services { get; set; }

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