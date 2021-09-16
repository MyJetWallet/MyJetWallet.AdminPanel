using Backoffice.TableStorage.Roles;
using MyAzureTableStorage;

namespace Backoffice.TableStorage
{
    public static class TableStorageFactory
    {
        public static TableStorageRoleRepository CreateRolesRepository(this string connectionString)
        {
            return
                new(
                    new AzureTableStorage<TableStorageBackofficeRoleModel>(() => connectionString, "UserRoles"));
        }
    }
}