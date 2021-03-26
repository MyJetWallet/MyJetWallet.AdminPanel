using System.Collections.Generic;
using System.Threading.Tasks;

namespace Backoffice.Services.Assets
{
    public interface IAssetItemManager
    {
        Task<List<AssetItem>> GetAll();
        Task CreateAsset(AssetItem item);
        Task UpdateAsset(AssetItem item);
        Task DeleteAsset(AssetItem item);

        Task AddBrand(AssetItem item, string brandId);
        Task RemoveBrand(AssetItem item, string brandId);
    }
}