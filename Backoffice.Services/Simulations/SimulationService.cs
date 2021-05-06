using Service.Simulation.FTX.Grpc;

namespace Backoffice.Services.Simulations
{
    public class SimulationService
    {
        public SimulationService(string name, ISimulationFtxTradingService tradingService, ISimulationFtxTradeHistoryService historyService)
        {
            Name = name;
            TradingService = tradingService;
            HistoryService = historyService;
        }
        
        public string Name { get; set; }
        public ISimulationFtxTradingService TradingService { get; init; }
        public ISimulationFtxTradeHistoryService HistoryService { get; init; }
    }
}