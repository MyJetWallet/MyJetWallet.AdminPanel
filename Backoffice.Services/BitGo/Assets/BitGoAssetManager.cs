using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MyJetWallet.BitGo.Settings.NoSql;
using MyJetWallet.BitGo.Settings.Services;
using Newtonsoft.Json;

namespace Backoffice.Services.BitGo.Assets
{
    public class BitGoAssetManager : IBitGoAssetManager
    {
        private readonly IBitGoAssetMapSettingsService _bitGoAssetMapSettingsService;
        private readonly ILogger<BitGoAssetManager> _logger;

        public BitGoAssetManager(
            IBitGoAssetMapSettingsService bitGoAssetMapSettingsService,
            ILogger<BitGoAssetManager> logger)
        {
            _bitGoAssetMapSettingsService = bitGoAssetMapSettingsService;
            _logger = logger;
        }

        public async Task<List<BitgoAssetMapEntity>> GetAll()
        {
            return (await _bitGoAssetMapSettingsService.GetAllAssetMapsAsync()).ToList();
        }

        public async Task CreateBitGoAsset(BitgoAssetMapEntity item)
        {
            try
            {
                _logger.LogInformation("Creating BitGo asset: {jsonText}", JsonConvert.SerializeObject(item));
                await _bitGoAssetMapSettingsService.CreateBitgoAssetMapEntityAsync(item.BrokerId, item.AssetSymbol,
                    item.BitgoWalletId, item.EnabledBitgoWalletIds, item.BitgoCoin);
                _logger.LogInformation("Created BitGo asset: {jsonText}", JsonConvert.SerializeObject(item));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Cannot create BitGo asset: {jsonText}", JsonConvert.SerializeObject(item));
                throw new Exception(ex.Message, ex);
            }
        }

        public async Task UpdateAsset(BitgoAssetMapEntity item)
        {
            try
            {
                _logger.LogInformation("Updating BitGo asset: {jsonText}", JsonConvert.SerializeObject(item));
                await _bitGoAssetMapSettingsService.UpdateBitgoAssetMapEntityAsync(item.BrokerId, item.AssetSymbol,
                    item.BitgoWalletId, item.EnabledBitgoWalletIds, item.BitgoCoin);
                _logger.LogInformation("Updated BitGo asset: {jsonText}", JsonConvert.SerializeObject(item));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Cannot update BitGo asset: {jsonText}", JsonConvert.SerializeObject(item));
                throw new Exception(ex.Message, ex);
            }
        }

        public async Task DeleteAsset(BitgoAssetMapEntity item)
        {
            try
            {
                _logger.LogInformation("Deleting BitGo asset: {jsonText}", JsonConvert.SerializeObject(item));
                await _bitGoAssetMapSettingsService.DeleteBitgoAssetMapEntityAsync(item.BrokerId, item.AssetSymbol);
                _logger.LogInformation("Deleted BitGo asset: {jsonText}", JsonConvert.SerializeObject(item));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Cannot delete BitGo asset: {jsonText}", JsonConvert.SerializeObject(item));
                throw new Exception(ex.Message, ex);
            }
        }
    }
}