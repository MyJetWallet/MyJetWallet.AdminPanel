using MyJetWallet.Sdk.ExternalMarketsSettings.Grpc;

namespace Backoffice.Services.ExternalMarkets
{
    public class ExternalMarketsSettingsService
    {
        public ExternalMarketsSettingsService(string name, IExternalMarketSettingsManagerGrpc manager)
        {
            Name = name;
            SettingsManager = manager;
        }

        public string Name { get; set; }
        public IExternalMarketSettingsManagerGrpc SettingsManager { get; init; }
    }
}