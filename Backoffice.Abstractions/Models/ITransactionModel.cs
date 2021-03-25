using System;

namespace Backoffice.Abstractions.Models
{
    public enum TransactionDomain
    {
        Trading,
        Others
    }

    public enum BoAccountTransactionType
    {
        OpenBuy,
        CloseBuy,
        OpenSell,
        CloseSell,
        Deposit,
        WithdrawRegistered,
        WithdrawMoneyReservation,
        WithdrawCanceledWithoutReservation,
        WithdrawCanceledWithReservation,
        WithdrawProcessed,
        Transfer,
        BonusDeposit,
        BonusWithdrawal,
        BonusCorrection,
        DepositVoid,
        DepositChargeBack,
    }

    public enum BoClosedPositionReason
    {
        ClientManual,
        ManagerClose,
        Sl,
        Tp,
        So,
        Split,
        Others,
    }

    public interface IBoTransactionModel
    {
        TransactionDomain TransactionDomain { get; }
        DateTime DateTime { get; }
        string Id { get; }
        string Instrument { get; }
        BoAccountTransactionType Operation { get; }
        double Volume { get; }
        double Pl { get; }
        double Swaps { get; }
        string Comment { get; }
        double OpenBalance { get; }
        double OpenBonus { get; }
        double CompanyPl { get; }
        double BonusDelta { get; }
        DateTime OpenDate { get; }
        double Commission { get; }
        string AccountId { get; }
        BoClosedPositionReason CloseReason { get; }
        double OpenPrice { get; }
        double ClosePrice { get; }
    }

    public class TransactionModel : IBoTransactionModel
    {
        public TransactionDomain TransactionDomain { get; set; }
        public DateTime DateTime { get; set; }
        public string Id { get; set; }
        public string Instrument { get; set; }
        public BoAccountTransactionType Operation { get; set; }
        public double Volume { get; set; }
        public double Pl { get; set; }
        public double Swaps { get; set; }
        public string Comment { get; set; }
        public double OpenBalance { get; set; }
        public double OpenBonus { get; set; }
        public double CompanyPl { get; set; }
        public double BonusDelta { get; set; }
        public DateTime OpenDate { get; set; }
        public double Commission { get; set; }
        public string AccountId { get; set; }
        public BoClosedPositionReason CloseReason { get; set; }
        public double OpenPrice { get; set; }
        public double ClosePrice { get; set; }
    }
}