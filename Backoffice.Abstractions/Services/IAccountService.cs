using System.Collections.Generic;
using System.Threading.Tasks;
using Backoffice.Abstractions.Models;

namespace Backoffice.Abstractions.Services
{
    public interface IAccountService
    {
        ValueTask<IAccountModel> GetAsync(string traderId, string accountId);
        ValueTask<IReadOnlyList<IAccountModel>> GetLiveAsync(string traderId);
        ValueTask<IReadOnlyList<IAccountModel>> GetDemoAsync(string traderId);
        ValueTask<IDealingInfoModel> GetDealingInfoAsync(string traderId, string accountId);
        ValueTask<string> BalanceCorrection(string traderId, string accountId, double delta, string comment);
        ValueTask TransferMoneyBetweenAccounts(string traderId,
            string fromAccountId, string toAccountId, string comment, double amount);
    }
}