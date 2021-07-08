using System.Collections.Generic;
using System.Threading.Tasks;
using Service.Fees.Domain.Models;

namespace Backoffice.Services.Fees.Assets
{
    public interface IAssetFeesManager
    {
        Task<List<AssetFees>> GetAll();
        Task CreateAssetFeesSettings(AssetFees item);
        Task UpdateAssetFeesSettings(AssetFees item);
        Task DeleteAssetFeesSettings(AssetFees item);
    }
}