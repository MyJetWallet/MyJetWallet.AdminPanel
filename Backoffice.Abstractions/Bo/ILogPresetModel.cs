using System.Collections.Generic;
using Backoffice.ReflectionSearch;

namespace Backoffice.Abstractions.Bo
{
    public interface ILogPresetModel
    {
        string BoUserId { get; }
        string FilterName { get; }
        string PageName { get; }
        string Filters { get; }
        IDictionary<string, SearchFilterItem> GetFilters();
    }
}