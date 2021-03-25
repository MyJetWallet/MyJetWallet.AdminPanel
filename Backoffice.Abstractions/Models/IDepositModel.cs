using System;
using System.Collections.Generic;

namespace Backoffice.Abstractions.Models
{
    public enum CrmDepositStatus
    {
        Registered,
        Paid,
        Failed,
    }

    public enum CrmVoidDepositReason
    {
        None,
        ChargeBack,
        Void,
        Fail
    }
    
    public interface IDepositModel
    {
        string Id { get; }
        string PaymentSystemTransactionId { get; }
        DateTime Registered { get; }
        DateTime LastUpdate { get; }
        string PaymentSystem { get; }
        string PaymentSystemCurrency { get; }
        double PaymentSystemAmount { get; }
        string Currency { get; }
        double Amount { get; }
        CrmDepositStatus Status { get; }
        string FullName { get; }
        string CountryCode { get; }
        string Email { get; }
        string TraderId { get; }
        string AccountId { get; }
        string VoidTransactionId { get; set; }
        CrmVoidDepositReason VoidDepositReason { get; }
        string Owner { get; }
        bool IsVoided { get; }
    }
    
    public class DepositModel : IDepositModel
    {
        public string Id { get; set;}
        public string PaymentSystemTransactionId { get; set;}
        public DateTime Registered { get; set;}
        public DateTime LastUpdate { get; set;}
        public string PaymentSystem { get; set;}
        public string PaymentSystemCurrency { get; set;}
        public double PaymentSystemAmount { get; set;}
        public string Currency { get; set;}
        public double Amount { get; set;}
        public CrmDepositStatus Status { get; set;}
        public string FullName { get; set;}
        public string CountryCode { get; set;}
        public string Email { get; set;}
        public string TraderId { get; set;}
        public string AccountId { get; set;}
        public string VoidTransactionId { get; set;}
        public CrmVoidDepositReason VoidDepositReason { get; set;}
        public string Owner { get; set;}
        public bool IsVoided { get; set; }
    }

    public interface IPaymentProviderSettingsModel
    {
        string PaymentProviderName { get; set; }
        string Brand { get; }
        int Weight { get; set; }
        IEnumerable<string> SupportedGeo { get; set; }
        IEnumerable<string> RestrictedGeo { get; set; }
        bool IsDeleted { get; set; }
    }
    
    public class PaymentProviderSettingsModel : IPaymentProviderSettingsModel
    {
        public string PaymentProviderName { get; set; }
        public string Brand { get; set; }
        public int Weight { get; set; }
        public IEnumerable<string> SupportedGeo { get; set; }
        public IEnumerable<string> RestrictedGeo { get; set; }
        public bool IsDeleted { get; set; }
    }

    public interface IPaymentSystemSettingsModel
    {
        string PaymentSystemName { get; set; }
        string Brand { get; }
        bool IsEnable { get; set; }
        IEnumerable<string> SupportedGeo { get; set; }
        IEnumerable<string> RestrictedGeo { get; set; }
        bool IsDeleted { get; set; }
    }

    public class PaymentSystemSettingsModel : IPaymentSystemSettingsModel
    {
        public string PaymentSystemName { get; set; }
        public string Brand { get; set; }
        public bool IsEnable { get; set; }
        public IEnumerable<string> SupportedGeo { get; set; }
        public IEnumerable<string> RestrictedGeo { get; set; }
        public bool IsDeleted { get; set; }
    }

}