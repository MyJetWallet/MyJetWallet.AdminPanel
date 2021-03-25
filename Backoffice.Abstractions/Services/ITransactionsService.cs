using System.Collections.Generic;
using System.Threading.Tasks;
using Backoffice.Abstractions.Models;

namespace Backoffice.Abstractions.Services
{
    public interface ITransactionsService
    {
        ValueTask<IReadOnlyList<IBoTransactionModel>> GetAsync(string traderId, string accountId);
    }
}