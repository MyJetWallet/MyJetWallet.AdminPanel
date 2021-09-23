using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MyJetWallet.BitGo.Settings.NoSql;
using MyJetWallet.BitGo.Settings.Services;
using Newtonsoft.Json;

namespace Backoffice.Services.BitGo.Coins
{
    public class BitGoCoinManager : IBitGoCoinManager
    {
        private readonly IBitGoCoinSettingsService _bitGoCoinSettingsService;
        private readonly ILogger<BitGoCoinManager> _logger;

        public BitGoCoinManager(
            IBitGoCoinSettingsService bitGoCoinSettingsService,
            ILogger<BitGoCoinManager> logger)
        {
            _bitGoCoinSettingsService = bitGoCoinSettingsService;
            _logger = logger;
        }

        public async Task CreateBitGoCoin(BitgoCoinEntity item)
        {
            try
            {
                _logger.LogInformation("Creating BitGo coin: {jsonText}", JsonConvert.SerializeObject(item));
                await _bitGoCoinSettingsService.CreateBitgoCoinEntityAsync(item.Coin, item.Accuracy,
                    item.RequiredConfirmations, item.IsMainNet);
                _logger.LogInformation("Created BitGo coin: {jsonText}", JsonConvert.SerializeObject(item));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Cannot create BitGo coin: {jsonText}", JsonConvert.SerializeObject(item));
                throw new Exception(ex.Message, ex);
            }
        }

        public async Task UpdateCoin(BitgoCoinEntity item)
        {
            try
            {
                _logger.LogInformation("Updating BitGo coin: {jsonText}", JsonConvert.SerializeObject(item));
                await _bitGoCoinSettingsService.UpdateBitgoCoinEntityAsync(item.Coin, item.Accuracy,
                    item.RequiredConfirmations, item.IsMainNet);
                _logger.LogInformation("Updated BitGo coin: {jsonText}", JsonConvert.SerializeObject(item));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Cannot create BitGo coin: {jsonText}", JsonConvert.SerializeObject(item));
                throw new Exception(ex.Message, ex);
            }
        }

        public async Task DeleteCoin(BitgoCoinEntity item)
        {
            try
            {
                _logger.LogInformation("Deleting BitGo coin: {jsonText}", JsonConvert.SerializeObject(item));
                await _bitGoCoinSettingsService.DeleteBitgoCoinEntityAsync(item.Coin);
                _logger.LogInformation("Deleted BitGo coin: {jsonText}", JsonConvert.SerializeObject(item));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Cannot create BitGo coin: {jsonText}", JsonConvert.SerializeObject(item));
                throw new Exception(ex.Message, ex);
            }
        }

        public async Task<List<BitgoCoinEntity>> GetAll()
        {
            return (await _bitGoCoinSettingsService.GetAllCoinsAsync()).ToList();
        }
    }
}