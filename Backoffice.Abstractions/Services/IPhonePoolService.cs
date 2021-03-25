using System.Collections.Generic;
using System.Threading.Tasks;
using Backoffice.Abstractions.Models;

namespace Backoffice.Abstractions.Services
{
    public interface IPhonePoolService
    {
        ValueTask<IReadOnlyList<IPhonePoolModel>> GetAsync();
        ValueTask<IPhonePoolModel> GetAsync(string id);
        Task DeleteAsync(string id);
        ValueTask InsertOrReplaceAsync(string id, string name, IEnumerable<(string name, string number)> numbers);
        ValueTask<BoCallResponseEnum> CallAsync(ICallModel callModel);
    }
}