using System;
using System.Collections.Generic;
using Backoffice.Abstractions.Bo;
using Microsoft.WindowsAzure.Storage.Table;
using Newtonsoft.Json;

namespace Backoffice.TableStorage.Roles
{
    public class TableStorageBackofficeRoleModel : TableEntity, IBackofficeRoleModel
    {
        public static string GeneratePartitionKey()
        {
            return "Role";
        }

        public static string GenerateRowKey(string id)
        {
            return id;
        }

        public string Id => RowKey;

        public string Name { get; set; }

        public string RightsData
        {
            get => JsonConvert.SerializeObject(Rights ?? Array.Empty<string>());
            set
            {
                if (string.IsNullOrEmpty(value))
                    Rights = Array.Empty<string>();

                try
                {
                    Rights = JsonConvert.DeserializeObject<string[]>(value);
                }
                catch (Exception)
                {
                    Rights = Array.Empty<string>();
                }
            }
        }

        public IEnumerable<string> Rights { get; private set; }

        public void Edit(IBackofficeRoleModel src)
        {
            Name = src.Name;
            Rights = src.Rights;
        }

        public static TableStorageBackofficeRoleModel Create(IBackofficeRoleModel src)
        {
            var result = new TableStorageBackofficeRoleModel
            {
                PartitionKey = GeneratePartitionKey(),
                RowKey = GenerateRowKey(Guid.NewGuid().ToString().Substring(0, 8).ToLower()),
            };

            result.Edit(src);

            return result;
        }
    }
}