using System;
using System.Collections.Generic;

namespace Backoffice.Abstractions.Models
{
    public interface IDealingInfoActivePositionModel
    {
        string Id { get; }
        string Type { get; }
        string Symbol { get; }
        double InvestAmount { get; }
        double Multiplier { get; }
        double Pnl { get; }
        double ToStopOutPercent { get; }
        double Swaps { get; }
        double Tp { get; }
        double Sl { get; }
        DateTime OpenDate { get; }
        double SoLvl { get; }
        double ReservedFundForToppingUp { get; }
    }

    public class DealingInfoActivePositionModel : IDealingInfoActivePositionModel
    {
        public string Id { get; set; }
        public string Type { get; set; }
        public string Symbol { get; set; }
        public double InvestAmount { get; set; }
        public double Multiplier { get; set; }
        public double Pnl { get; set; }
        public double ToStopOutPercent { get; set; }
        public double Swaps { get; set; }
        public double Tp { get; set; }
        public double Sl { get; set; }
        public DateTime OpenDate { get; set; }
        public double SoLvl { get; set; }
        public double ReservedFundForToppingUp { get; set; }
    }

    public interface IClosedPositionModel
    {
        string Id { get; }
        string Type { get; }
        string Symbol { get; }
        double InvestAmount { get; }
        double Multiplier { get; }
        double Pnl { get; }
        double ToStopOutPercent { get; }
        double Swaps { get; }
        double Tp { get; }
        double Sl { get; }
        DateTime OpenDate { get; }
        double SoLvl { get; }
        double ReservedFundForToppingUp { get; }
        string CloseReason { get; }
        DateTime CloseDate { get; }
    }

    public class ClosedPositionModel : IClosedPositionModel
    {
        public string Id { get; set; }
        public string Type { get; set; }
        public string Symbol { get; set; }
        public double InvestAmount { get; set; }
        public double Multiplier { get; set; }
        public double Pnl { get; set; }
        public double ToStopOutPercent { get; set; }
        public double Swaps { get; set; }
        public double Tp { get; set; }
        public double Sl { get; set; }
        public DateTime OpenDate { get; set; }
        public double SoLvl { get; set; }
        public double ReservedFundForToppingUp { get; set; }
        public string CloseReason { get; set; }
        public DateTime CloseDate { get; set; }
    }

    public interface IDealingInfoModel
    {
       DateTime LastPositionPlacedDate { get; }
       double TotalInvestment { get; }
       double RemainingBalance { get; }
       double CurrentPnlWithSwaps { get; }
       double SwapsSum { get; }
       double CommissionSum { get; }
       double DepositsSum { get; }
       double WithdrawalsSum { get; }
       double TotalPl { get; }
       int ActivePositionsCount { get; }
       double NetDeposit { get; }
       double TradedVolume { get; }
       double TotalSwaps { get; }
       IEnumerable<IDealingInfoActivePositionModel> ActivePosition { get; }
       IEnumerable<IClosedPositionModel> ClosedPosition { get; }
    }
    
    public class DealingInfoModel : IDealingInfoModel
    {
        public DateTime LastPositionPlacedDate { get; set;}
        public double TotalInvestment { get; set;}
        public double RemainingBalance { get; set;}
        public double CurrentPnlWithSwaps { get; set;}
        public double SwapsSum { get; set;}
        public double CommissionSum { get; set;}
        public double DepositsSum { get; set;}
        public double WithdrawalsSum { get; set;}
        public double TotalPl { get; set;}
        public int ActivePositionsCount { get; set;}
        public double NetDeposit { get; set;}
        public double TradedVolume { get; set;}
        public double TotalSwaps { get; set;}
        public IEnumerable<IDealingInfoActivePositionModel> ActivePosition { get; set;}
        public IEnumerable<IClosedPositionModel> ClosedPosition { get; set;}
    }
}