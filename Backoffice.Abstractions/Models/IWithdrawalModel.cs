using System;

namespace Backoffice.Abstractions.Models
{
    public enum CrmWithdrawalStatus
    {
        Pending,
        PendingWithReservation,
        Processed,
        Canceled
    }

    public interface IWithdrawalModel
    {
        string Id { get; }
        DateTime Registered { get; }
        string TraderId { get; }
        string AccountId { get; }
        string Owner { get; }
        string Currency { get; }
        double Amount { get; }
        string PaymentSystem { get; }
        string PaymentSystemData { get; }
        CrmWithdrawalStatus Status { get; }
        string FullName { get; }
        string CountryCode { get; }
        string Email { get; }
        string Comment { get; }
        string AssignedBackOfficeUserId { get; }
        string BackOfficeUserId { get; }
    }
    
    public class WithdrawalModel : IWithdrawalModel
    {
        public string Id { get; set;}
        public DateTime Registered { get; set;}
        public string TraderId { get; set;}
        public string AccountId { get; set;}
        public string Owner { get; set;}
        public string Currency { get; set;}
        public double Amount { get; set;}
        public string PaymentSystem { get; set;}
        public string PaymentSystemData { get; set;}
        public CrmWithdrawalStatus Status { get; set;}
        public string FullName { get; set;}
        public string CountryCode { get; set;}
        public string Email { get; set;}
        public string Comment { get; set;}
        public string AssignedBackOfficeUserId { get; set;}
        public string BackOfficeUserId { get; set;}
    }
}