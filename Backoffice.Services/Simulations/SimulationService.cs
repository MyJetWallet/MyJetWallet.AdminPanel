using Service.Simulation.Grpc;

namespace Backoffice.Services.Simulations
{
    public class SimulationService
    {
        public SimulationService(string name, ISimulationTradingService tradingService,
            ISimulationTradeHistoryService historyService)
        {
            Name = name;
            TradingService = tradingService;
            HistoryService = historyService;
        }

        public string Name { get; set; }
        public ISimulationTradingService TradingService { get; init; }
        public ISimulationTradeHistoryService HistoryService { get; init; }
    }
}