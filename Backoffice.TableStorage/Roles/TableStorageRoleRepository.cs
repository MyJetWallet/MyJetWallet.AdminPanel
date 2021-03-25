using System.Collections.Generic;
using System.Threading.Tasks;
using Backoffice.Abstractions.Bo;
using DotNetCoreDecorators;
using MyAzureTableStorage;

namespace Backoffice.TableStorage.Roles
{
    public class TableStorageRoleRepository : IBackofficeRolesRepository
    {
        private readonly IAzureTableStorage<TableStorageBackofficeRoleModel> _tableStorage;

        public TableStorageRoleRepository(IAzureTableStorage<TableStorageBackofficeRoleModel> tableStorage)
        {
            _tableStorage = tableStorage;
        }
        
        public async Task AddAsync(IBackofficeRoleModel backOfficeRole)
        {
            var newEntity = TableStorageBackofficeRoleModel.Create(backOfficeRole);
            await _tableStorage.InsertAsync(newEntity);
        }

        public async Task EditAsync(IBackofficeRoleModel data)
        {
            var partitionKey = TableStorageBackofficeRoleModel.GeneratePartitionKey();
            var rowKey = TableStorageBackofficeRoleModel.GenerateRowKey(data.Id);

            await _tableStorage.ReplaceAsync(partitionKey, rowKey, entity =>
            {
                entity.Edit(data);
                return entity;
            });
        }

        public async Task<IEnumerable<IBackofficeRoleModel>> GetAllRolesAsync()
        {
            var partitionKey = TableStorageBackofficeRoleModel.GeneratePartitionKey();
            return await _tableStorage.GetAsync(partitionKey).AsListAsync();
        }

        public async Task<IBackofficeRoleModel> GetRoleAsync(string id)
        {
            var partitionKey = TableStorageBackofficeRoleModel.GeneratePartitionKey();
            var rowKey = TableStorageBackofficeRoleModel.GenerateRowKey(id);
            return await _tableStorage.GetAsync(partitionKey, rowKey);
        }
    }
}