using System;
using System.Collections.Generic;
using System.Text;
using Backoffice.Abstractions.Bo;
using Backoffice.ReflectionSearch;
using Microsoft.WindowsAzure.Storage.Table;
using Newtonsoft.Json;

namespace Backoffice.TableStorage.FilterPresets
{
    public class TableStorageFilterPresetModel: TableEntity, ILogPresetModel
    {
        public static string GeneratePartitionKey(string boUserId)
        {
            return boUserId;
        }
        
        public static string GenerateRowKey(string pageName, string filterName)
        {
            return $"{pageName}-{filterName}";
        }

        public string BoUserId => PartitionKey;
        
        public string FilterName { get; set; }
        public string PageName { get; set; }
        public string Filters { get; set; }

        public IDictionary<string, SearchFilterItem> GetFilters()
        {
            return DecodeFilters(Filters);
        }

        public static TableStorageFilterPresetModel Create(string boUserId, string pageName, string filterName,
            IDictionary<string, SearchFilterItem> filters)
        {
            return new TableStorageFilterPresetModel
            {
                PartitionKey = GeneratePartitionKey(boUserId),
                RowKey = GenerateRowKey(pageName, filterName),
                FilterName = filterName,
                PageName = pageName,
                Filters = EncodeFilters(filters)
            };
        }

        private static string EncodeFilters(IDictionary<string, SearchFilterItem> filters)
        {
            var bytesData = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(filters));
            return Convert.ToBase64String(bytesData);
        }

        private static IDictionary<string, SearchFilterItem> DecodeFilters(string filters)
        {
            var bytesData = Convert.FromBase64String(filters);
            return JsonConvert.DeserializeObject<IDictionary<string, SearchFilterItem>>(
                Encoding.UTF8.GetString(bytesData));
        }
    }
}