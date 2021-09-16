using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Backoffice.Abstractions.Bo;
using DotNetCoreDecorators;
using MyAzureTableStorage;

namespace Backoffice.TableStorage.Roles
{
    public class TableStorageRoleRepository : IBackofficeRolesRepository, IStartable
    {
        private readonly IAzureTableStorage<TableStorageBackofficeRoleModel> _tableStorage;

        private List<IBackofficeRoleModel> _roles = new();

        public TableStorageRoleRepository(IAzureTableStorage<TableStorageBackofficeRoleModel> tableStorage)
        {
            _tableStorage = tableStorage;
        }
        
        public async Task AddAsync(IBackofficeRoleModel backOfficeRole)
        {
            var newEntity = TableStorageBackofficeRoleModel.Create(backOfficeRole);
            await _tableStorage.InsertAsync(newEntity);
            await RefreshRoles();
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

            await RefreshRoles();
        }
        
        public async Task DeleteAsync(IBackofficeRoleModel data)
        {
            await _tableStorage.DeleteAsync(TableStorageBackofficeRoleModel.Create(data.Id));
            await RefreshRoles();
        }

        public async Task<IReadOnlyList<IBackofficeRoleModel>> GetAllRolesAsync()
        {
            return _roles;
        }

        public async Task<IBackofficeRoleModel> GetRoleAsync(string id)
        {
            return _roles.FirstOrDefault(e => e.Id == id);
        }

        public void Start()
        {
            RefreshRoles().GetAwaiter().GetResult();
        }

        private async Task RefreshRoles()
        {
            var partitionKey = TableStorageBackofficeRoleModel.GeneratePartitionKey();
            var data = await _tableStorage.GetAsync(partitionKey).AsListAsync();
            _roles = data.Cast<IBackofficeRoleModel>().ToList();
        }
    }
}