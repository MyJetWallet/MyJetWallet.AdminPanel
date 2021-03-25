using System.Collections.Generic;
using System.Threading.Tasks;
using Backoffice.Abstractions.Bo;
using Backoffice.ReflectionSearch;
using Backoffice.Services;
using DotNetCoreDecorators;
using MyAzureTableStorage;

namespace Backoffice.TableStorage.FilterPresets
{
    public class TableStorageLogsPresetRepository : IBackofficeFiltersPresetRepository
    {
        private readonly IAzureTableStorage<TableStorageFilterPresetModel> _tableStorage;
        
        public TableStorageLogsPresetRepository(IAzureTableStorage<TableStorageFilterPresetModel> tableStorage)
        {
            _tableStorage = tableStorage;
        }

        public async Task InsertOrReplaceAsync(string boUserId, string pageName, string name,
            IDictionary<string, SearchFilterItem> filter)
        {
            await _tableStorage.InsertOrReplaceAsync(
                TableStorageFilterPresetModel
                    .Create(boUserId, pageName, name, filter)
                );
        }

        public async Task<IEnumerable<ILogPresetModel>> GetFiltersForPage(string boUserId, string pageName)
        {
            var pk = TableStorageFilterPresetModel.GeneratePartitionKey(boUserId);
            var result =  _tableStorage.GetAsync(pk);

            return await result.WhereAsync(itm => itm.PageName == pageName).AsListAsync();
        }
    }
}