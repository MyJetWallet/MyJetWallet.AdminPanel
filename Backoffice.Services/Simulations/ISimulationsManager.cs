using System.Collections.Generic;
using System.Linq;
using Service.Simulation.FTX.Client;
using Service.Simulation.FTX.Grpc;

namespace Backoffice.Services.Simulations
{
    public interface ISimulationsManager
    {
        List<SimulationService> GetSimulations();
    }

    public class SimulationsManager : ISimulationsManager
    {
        private List<SimulationService> _services { get; set; }
        
        public SimulationsManager(Dictionary<string, string> settings)
        {
            _services = new List<SimulationService>();
            foreach (var setting in settings)
            {
                SimulationFTXClientFactory ftxClientFactory = new SimulationFTXClientFactory(setting.Value);
                var service = new SimulationService(
                    setting.Key,
                    ftxClientFactory.GetSimulationFtxTradingService(),
                    ftxClientFactory.GetSimulationFtxTradeHistoryService());
                
                _services.Add(service);
            }
        }
        
        public List<SimulationService> GetSimulations()
        {
            return _services.ToList();
        }
    }
}