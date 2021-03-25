using System;
using Backoffice.ReflectionSearch;
using MyCrm.Deposits.Grpc.Models;

namespace Backoffice.Abstractions.Models
{
    public interface IPaymentReportModel
    {
        DateTime TransactionDate { get; }
        DateTime TraderRegistrationDate { get; }
        string Office { get; }
        string AffiliateId { get; }
        string TransactionType { get; }
        bool? IsInit { get; }
        string RetentionManager { get; }
        string AccountManagerWhenCreated { get; }
        string ConversionStatus { get; }
        string KYC { get; }
        string TradingStatusWhenCreated { get; }
        string PaymentTransactionId { get; }
        string TraderId { get; }
        string AccountId { get; }
        string FirstName { get; }
        string LastName { get; }
        string Email { get; }
        string Country { get; }
        string PsTransactionId { get; }
        string Status { get; }
        double Amount { get; }
        string Currency { get; }
        string PaymentProvider { get; }
        string PaymentSystem { get; }
        double PsAmount { get; }
        string PsCurrency { get; }
        bool IsInternal { get; }
        string VoidTransactionId { get; }
        VoidDepositReason VoidDepositReason { get; }
        bool IsVoided { get; }
        DateTime LastUpdate { get; }
        string CountryOfCitizenship { get; }
        string CountryOfResidence { get; }
        string CountryOfRegistration { get; }
    }

    public class PaymentReportModel : IPaymentReportModel
    {
        [Searchable] public DateTime TransactionDate { get; set; }
        [Searchable] public DateTime TraderRegistrationDate { get; set; }
        [Searchable] public string Office { get; set; }
        [Searchable] public string AffiliateId { get; set; }
        [Searchable] public string TransactionType { get; set; }
        public bool? IsInit { get; set; }
        [Searchable] public string RetentionManager { get; set; }
        [Searchable] public string AccountManagerWhenCreated { get; set; }
        [Searchable] public string ConversionStatus { get; set; }
        [Searchable] public string KYC { get; set; }
        [Searchable] public string TradingStatusWhenCreated { get; set; }
        [Searchable] public string PaymentTransactionId { get; set; }
        [Searchable] public string TraderId { get; set; }
        [Searchable] public string AccountId { get; set; }
        [Searchable] public string FirstName { get; set; }
        [Searchable] public string LastName { get; set; }
        [Searchable] public string Email { get; set; }
        [Searchable] public string Country { get; set; }
        [Searchable] public string PsTransactionId { get; set; }
        [Searchable] public string Status { get; set; }
        [Searchable] public double Amount { get; set; }
        [Searchable] public string Currency { get; set; }
        [Searchable] public string PaymentProvider { get; set; }
        [Searchable] public string PaymentSystem { get; set; }
        [Searchable] public double PsAmount { get; set; }
        [Searchable] public string PsCurrency { get; set; }
        public bool IsInternal { get; set; }
        [Searchable] public string VoidTransactionId { get; set; }
        public VoidDepositReason VoidDepositReason { get; set; }
        public bool IsVoided { get; set; }
        public DateTime LastUpdate { get; set; }
        public string CountryOfCitizenship { get; set;}
        public string CountryOfResidence { get; set;}
        public string CountryOfRegistration { get; set;}

        public static PaymentReportModel Create(IPaymentReportModel src)
        {
            return new PaymentReportModel
            {
                TraderId = src.TraderId,
                AccountId = src.AccountId,
                AccountManagerWhenCreated = src.AccountManagerWhenCreated,
                AffiliateId = src.AffiliateId,
                Amount = src.Amount,
                ConversionStatus = src.ConversionStatus,
                Country = src.Country,
                Currency = src.Currency,
                Email = src.Email,
                FirstName = src.FirstName,
                IsInit = src.IsInit,
                TransactionDate = src.TransactionDate,
                KYC = src.KYC,
                LastName = src.LastName,
                Office = src.Office,
                PaymentProvider = src.PaymentProvider,
                PaymentSystem = src.PaymentSystem,
                PaymentTransactionId = src.PaymentTransactionId,
                PsTransactionId = src.PsTransactionId,
                RetentionManager = src.RetentionManager,
                Status = src.Status,
                TraderRegistrationDate = src.TraderRegistrationDate,
                TradingStatusWhenCreated = src.TradingStatusWhenCreated,
                TransactionType = src.TransactionType,
                IsInternal = src.IsInternal,
                PsAmount = src.PsAmount,
                PsCurrency = src.PsCurrency,
                VoidDepositReason = VoidDepositReason.None,
                VoidTransactionId = src.VoidTransactionId,
                IsVoided = src.IsVoided,
                LastUpdate = src.LastUpdate
            };
        }
    }
}