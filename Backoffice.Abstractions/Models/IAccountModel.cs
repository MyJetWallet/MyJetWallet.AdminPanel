using System;

namespace Backoffice.Abstractions.Models
{
    public interface IAccountModel
    {
        DateTime Registered { get; }
        string TraderId { get; }
        public string AccountId { get; }
        string Currency { get; }
        double Balance { get; }
        double Bonus { get; }
        double Equity { get; }
        double ReservedForWithdrawals { get; }
        double FreeToWithdraw { get; }
        bool IsLive { get; }
        long Login { get; }
        int Leverage { get; }
        string Group { get; }
        string AccountType { get; }
        double Margin { get; }
        double MarginFree { get; }
        DateTime? FirstDeposit { get; }
        double NetDeposit { get; }
        double Profit { get; }
    }

    public class AccountModel : IAccountModel
    {
        public DateTime Registered { get; set; }
        public string TraderId { get; set; }
        public string AccountId { get; set; }
        public string Currency { get; set; }
        public double Balance { get; set; }
        public double Bonus { get; set; }
        public double Equity { get; set; }
        public double ReservedForWithdrawals { get; set; }
        public double FreeToWithdraw { get; set; }
        public bool IsLive { get; set; }
        public long Login { get; set; }
        public int Leverage { get; set; }
        public string Group { get; set; }
        public string AccountType { get; set; }
        public double Margin { get; set; }
        public double MarginFree { get; set; }
        public DateTime? FirstDeposit { get; set; }
        public double NetDeposit { get; set; }
        public double Profit { get; set; }
    }
}