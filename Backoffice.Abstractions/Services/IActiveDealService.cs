using System.Collections.Generic;
using System.Threading.Tasks;
using Backoffice.Abstractions.Models;

namespace Backoffice.Abstractions.Services
{
    public interface IActiveDealService
    {
        public Task<IReadOnlyList<IActiveDealModel>> GetByTraderIdAsync(string traderId);
    }
}