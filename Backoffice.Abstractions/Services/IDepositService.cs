using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Backoffice.Abstractions.Models;

namespace Backoffice.Abstractions.Services
{
    public interface ISearchDepositModel
    {
        string BoUserId { get; }
        DateTime From { get; }
        DateTime To { get; }
        bool IncludeInternal { get; }
        CrmDepositStatus[] Statuses { get; }
    }

    public class SearchDepositDto : ISearchDepositModel
    {
        public string BoUserId { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public bool IncludeInternal { get; set; }
        public CrmDepositStatus[] Statuses { get; set; }
    }
    
    
    public interface IFundAccountDto
    {
        string TraderId { get; }
        string AccountId { get; }
        double Amount { get; }
        string PaymentSystemId { get; }
        string PsCurrency { get; }
        double PsAmount { get; }
        string Comment { get; }
        string PsTransactionId { get; }
    }

    public class FundAccountDto : IFundAccountDto
    {
        public string TraderId { get; set;}
        public string AccountId { get; set;}
        public double Amount { get; set;}
        public string PaymentSystemId { get; set;}
        public string PsCurrency { get; set;}
        public double PsAmount { get; set;}
        public string Comment { get; set;}
        public string PsTransactionId { get; set;}
    }

    public interface IDepositService
    {
        ValueTask<IReadOnlyList<IDepositModel>> GetSuccessfulAsync(string boUserId, DateTime from, DateTime to,
            bool includeInternal);

        ValueTask<IReadOnlyList<IDepositModel>> GetAllAsync(string boUserId, DateTime from, DateTime to,
            bool includeInternal);

        ValueTask<IReadOnlyList<IDepositModel>> GetAsync(ISearchDepositModel searchDepositModel);

        ValueTask<IReadOnlyList<IDepositModel>> GetByTraderIdAsync(string boUserId, string traderId,
            bool successfulOnly);

        ValueTask<IReadOnlyList<IDepositModel>> FindAsync(string boUserId, string transactionId);
        ValueTask<IDepositModel> GetAsync(string boUserId, string transactionId);
        
        ValueTask FundAccount(IFundAccountDto fundRequest);
        
        ValueTask<IReadOnlyList<string>> GetAvailablePaymentProvidersAsync();
        ValueTask<IReadOnlyList<string>> GetSupportedBrandsAsync();
        ValueTask<IReadOnlyList<IPaymentProviderSettingsModel>> GetPaymentProviderSettingsAsync();
        ValueTask SetPaymentProvidersSettingsAsync(IEnumerable<IPaymentProviderSettingsModel> settings);

        ValueTask<IReadOnlyList<string>> GetAvailablePaymentSystemsAsync();
        ValueTask<IReadOnlyList<IPaymentSystemSettingsModel>> GetPaymentSystemSettingsAsync();
        ValueTask SetPaymentSystemSettingsAsync(IEnumerable<IPaymentSystemSettingsModel> settings);
        ValueTask MakeDepositVoid(string depositId, string comment);
    }
}