using System.Collections.Generic;
using System.Threading.Tasks;
using MyJetWallet.BitGo.Settings.NoSql;

namespace Backoffice.Services.BitGo.Assets
{
    public interface IBitGoAssetManager
    {
        Task<List<BitgoAssetMapEntity>> GetAll();
        Task CreateBitGoAsset(BitgoAssetMapEntity item);
        Task UpdateAsset(BitgoAssetMapEntity item);
        Task DeleteAsset(BitgoAssetMapEntity item);
    }
}