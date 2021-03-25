using System.Collections.Generic;
using System.Threading.Tasks;
using Backoffice.ReflectionSearch;

namespace Backoffice.Abstractions.Bo
{
    public interface IBackofficeFiltersPresetRepository
    {
        Task InsertOrReplaceAsync(string boUserId, string pageName, string name,
            IDictionary<string, SearchFilterItem> filter);

        Task<IEnumerable<ILogPresetModel>> GetFiltersForPage(string boUserId, string pageName);
    }
}