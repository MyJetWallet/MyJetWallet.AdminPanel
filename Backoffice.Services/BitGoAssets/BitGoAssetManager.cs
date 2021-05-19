using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MyJetWallet.BitGo.Settings.NoSql;
using MyNoSqlServer.Abstractions;
using Newtonsoft.Json;

namespace Backoffice.Services.BitGoAssets
{
    public class BitGoAssetManager : IBitGoAssetManager
    {
        private readonly IMyNoSqlServerDataWriter<BitgoAssetMapEntity> _assetMap;
        private readonly IMyNoSqlServerDataWriter<BitgoCoinEntity> _bitgoCoins;
        private readonly ILogger<BitGoAssetManager> _logger;

        public BitGoAssetManager(
            IMyNoSqlServerDataWriter<BitgoAssetMapEntity> assetMap,
            IMyNoSqlServerDataWriter<BitgoCoinEntity> bitgoCoins,
            ILogger<BitGoAssetManager> logger)
        {
            _assetMap = assetMap;
            _bitgoCoins = bitgoCoins;
            _logger = logger;
        }

        public async Task<List<BitgoAssetMapEntity>> GetAll()
        {
            var entities = await _assetMap.GetAsync();
            return entities.ToList();
        }

        public async Task CreateBitGoAsset(BitgoAssetMapEntity item)
        {
            _logger.LogInformation("Creating BitGo asset: {jsonText}", JsonConvert.SerializeObject(item));

            if (string.IsNullOrEmpty(item.BrokerId))
                throw new Exception("Cannot create BitGo asset. BrokerId cannot be empty");
            if (string.IsNullOrEmpty(item.AssetSymbol))
                throw new Exception("Cannot create BitGo asset. AssetSymbol cannot be empty");
            if (string.IsNullOrEmpty(item.BitgoWalletId))
                throw new Exception("Cannot create BitGo asset. BitgoWalletId cannot be empty");
            if (string.IsNullOrEmpty(item.BitgoCoin))
                throw new Exception("Cannot create BitGo asset. BitgoCoin cannot be empty");

            var coin = await _bitgoCoins.GetAsync(BitgoCoinEntity.GeneratePartitionKey(),
                BitgoCoinEntity.GenerateRowKey(item.BitgoCoin));
            if (coin == null) throw new Exception("Cannot create BitGo asset. Unknown BitgoCoin.");

            var existingItem = await _assetMap.GetAsync(item.PartitionKey, item.RowKey);
            if (existingItem != null) throw new Exception("Cannot create BitGo asset. Already exist");

            await _assetMap.InsertAsync(item);

            _logger.LogInformation("Created BitGo asset: {jsonText}", JsonConvert.SerializeObject(item));
        }

        public async Task UpdateAsset(BitgoAssetMapEntity item)
        {
            _logger.LogInformation("Updating BitGo asset: {jsonText}", JsonConvert.SerializeObject(item));

            if (string.IsNullOrEmpty(item.BrokerId))
                throw new Exception("Cannot create BitGo asset. BrokerId cannot be empty");
            if (string.IsNullOrEmpty(item.AssetSymbol))
                throw new Exception("Cannot create BitGo asset. AssetSymbol cannot be empty");
            if (string.IsNullOrEmpty(item.BitgoWalletId))
                throw new Exception("Cannot create BitGo asset. BitgoWalletId cannot be empty");
            if (string.IsNullOrEmpty(item.BitgoCoin))
                throw new Exception("Cannot create BitGo asset. BitgoCoin cannot be empty");

            var coin = await _bitgoCoins.GetAsync(BitgoCoinEntity.GeneratePartitionKey(),
                BitgoCoinEntity.GenerateRowKey(item.BitgoCoin));
            if (coin == null) throw new Exception("Cannot create BitGo asset. Unknown BitgoCoin.");

            var entity = await _assetMap.GetAsync(item.PartitionKey, item.RowKey);
            if (entity == null) throw new Exception("Cannot update BitGo asset. Asset not found");

            await _assetMap.InsertOrReplaceAsync(item);

            _logger.LogInformation("Updated BitGo asset: {jsonText}", JsonConvert.SerializeObject(item));
        }

        public async Task DeleteAsset(BitgoAssetMapEntity item)
        {
            _logger.LogInformation("Deleting BitGo asset: {jsonText}", JsonConvert.SerializeObject(item));
            if (string.IsNullOrEmpty(item.BrokerId))
                throw new Exception("Cannot delete BitGo asset. BrokerId cannot be empty");
            if (string.IsNullOrEmpty(item.AssetSymbol))
                throw new Exception("Cannot delete BitGo asset. AssetSymbol cannot be empty");

            var entity = await _assetMap.GetAsync(item.PartitionKey, item.RowKey);

            if (entity != null)
            {
                await _assetMap.DeleteAsync(item.PartitionKey, item.RowKey);
            }

            _logger.LogInformation("Deleted BitGo asset: {jsonText}", JsonConvert.SerializeObject(item));
        }
    }
}