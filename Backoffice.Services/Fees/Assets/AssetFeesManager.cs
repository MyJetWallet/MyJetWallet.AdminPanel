using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Service.Fees.Domain.Models;
using Service.Fees.Grpc;
using Service.Fees.Grpc.Models;

// ReSharper disable InconsistentLogPropertyNaming

namespace Backoffice.Services.Fees.Assets
{
    public class AssetFeesManager : IAssetFeesManager
    {
        private readonly IAssetFeesSettingsService _assetFeesSettingsService;
        private readonly ILogger<AssetFeesManager> _logger;

        public AssetFeesManager(
            IAssetFeesSettingsService assetFeesSettingsService,
            ILogger<AssetFeesManager> logger)
        {
            _assetFeesSettingsService = assetFeesSettingsService;
            _logger = logger;
        }

        public async Task<List<AssetFees>> GetAll()
        {
            return await _assetFeesSettingsService.GetAssetFeesSettingsList();
        }

        public async Task CreateAssetFeesSettings(AssetFees item)
        {
            try
            {
                _logger.LogInformation("Creating asset fees settings: {jsonText}", JsonConvert.SerializeObject(item));
                await _assetFeesSettingsService.AddAssetFeesSettings(item);
                _logger.LogInformation("Created asset fees settings: {jsonText}", JsonConvert.SerializeObject(item));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Cannot create asset fees settings: {jsonText}", JsonConvert.SerializeObject(item));
                throw new Exception(ex.Message, ex);
            }
        }

        public async Task UpdateAssetFeesSettings(AssetFees item)
        {
            try
            {
                _logger.LogInformation("Updating asset fees settings: {jsonText}", JsonConvert.SerializeObject(item));
                await _assetFeesSettingsService.UpdateAssetFeesSettings(item);
                _logger.LogInformation("Updated asset fees settings: {jsonText}", JsonConvert.SerializeObject(item));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Cannot update asset fees settings: {jsonText}", JsonConvert.SerializeObject(item));
                throw new Exception(ex.Message, ex);
            }
        }

        public async Task DeleteAssetFeesSettings(AssetFees item)
        {
            try
            {
                _logger.LogInformation("Deleting asset fees settings: {jsonText}", JsonConvert.SerializeObject(item));
                await _assetFeesSettingsService.RemoveAssetFeesSettings(new RemoveAssetFeesRequest
                {
                    BrokerId = item.BrokerId,
                    AssetId = item.AssetId,
                    OperationType = item.OperationType
                });
                _logger.LogInformation("Deleted asset fees settings: {jsonText}", JsonConvert.SerializeObject(item));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Cannot delete asset fees settings: {jsonText}", JsonConvert.SerializeObject(item));
                throw new Exception(ex.Message, ex);
            }
        }
    }
}