using System.Collections.Generic;
using System.Linq;
using Service.Simulation.Binance.Client;
using Service.Simulation.FTX.Client;

namespace Backoffice.Services.Simulations
{
    public interface ISimulationsManager
    {
        List<SimulationService> GetSimulations();
    }

    public class SimulationsManager : ISimulationsManager
    {
        private List<SimulationService> _services { get; set; }

        public SimulationsManager(Dictionary<string, Dictionary<string, string>> settings)
        {
            _services = new List<SimulationService>();
            foreach (var setting in settings["FTX"])
            {
                SimulationFtxClientFactory ftxClientFactory = new SimulationFtxClientFactory(setting.Value);
                var service = new SimulationService(
                    setting.Key,
                    ftxClientFactory.GetSimulationFtxTradingService(),
                    ftxClientFactory.GetSimulationFtxTradeHistoryService());

                _services.Add(service);
            }

            foreach (var setting in settings["Binance"])
            {
                SimulationBinanceClientFactory binanceClientFactory = new SimulationBinanceClientFactory(setting.Value);
                var service = new SimulationService(
                    setting.Key,
                    binanceClientFactory.GetSimulationBinanceTradingService(),
                    binanceClientFactory.GetSimulationBinanceTradeHistoryService());

                _services.Add(service);
            }
        }

        public List<SimulationService> GetSimulations()
        {
            return _services.ToList();
        }
    }
}