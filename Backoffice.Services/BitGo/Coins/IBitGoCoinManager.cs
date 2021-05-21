using System.Collections.Generic;
using System.Threading.Tasks;
using MyJetWallet.BitGo.Settings.NoSql;

namespace Backoffice.Services.BitGo.Coins
{
    public interface IBitGoCoinManager
    {
        Task<List<BitgoCoinEntity>> GetAll();
        Task CreateBitGoCoin(BitgoCoinEntity item);
        Task UpdateCoin(BitgoCoinEntity item);
        Task DeleteCoin(BitgoCoinEntity item);
    }
}