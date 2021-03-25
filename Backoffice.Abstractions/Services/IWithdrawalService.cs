using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Backoffice.Abstractions.Models;

namespace Backoffice.Abstractions.Services
{
    public interface ISearchWithdrawalModel
    {
        DateTime From { get; }
        DateTime To { get; }
        CrmWithdrawalStatus[] Statuses { get; }
    }

    public interface IWithdrawalService
    {
        ValueTask<IReadOnlyList<IWithdrawalModel>> GetPendingWithdrawsAsync();
        ValueTask<IWithdrawalModel> CancelWithdrawAsync(string transactionId, string comment);
        ValueTask<IWithdrawalModel> CancelWithReservationAsync(string transactionId, string comment);
        ValueTask<IWithdrawalModel> ProcessWithdrawAsync(string transactionId, string comment);
        ValueTask<IWithdrawalModel> ReserveMoneyAsync(string transactionId, string comment);
        ValueTask<IReadOnlyList<IWithdrawalModel>> GetWithdrawsAsync(ISearchWithdrawalModel searchModel);
        ValueTask<IWithdrawalModel> GetByIdAsync(string id);
        ValueTask UpdateWithdrawDetailsAsync(string id, string paymentSystemId, string withdrawData);
        ValueTask Create(string traderId, string accountId, string currency, double amount, string type);
    }
}