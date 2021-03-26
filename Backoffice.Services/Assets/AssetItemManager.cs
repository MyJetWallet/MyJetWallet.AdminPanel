using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MyJetWallet.Domain;
using MyJetWallet.Domain.Assets;
using MyNoSqlServer.Abstractions;
using Newtonsoft.Json;
using Service.AssetsDictionary.Domain.Models;
using Service.AssetsDictionary.Grpc;
using Service.AssetsDictionary.Grpc.Models;
using Service.AssetsDictionary.MyNoSql;

namespace Backoffice.Services.Assets
{
    public class AssetItemManager : IAssetItemManager
    {
        private readonly IAssetsDictionaryService _assetsDictionaryService;
        private readonly IBrandAssetsAndInstrumentsService _brandAssetsAndInstrumentsService;
        private readonly IMyNoSqlServerDataWriter<AssetPaymentSettingsNoSqlEntity> _paymentsWriter;
        private readonly ILogger<AssetItemManager> _logger;

        public AssetItemManager(
            IAssetsDictionaryService assetsDictionaryService,
            IBrandAssetsAndInstrumentsService brandAssetsAndInstrumentsService,
            IMyNoSqlServerDataWriter<AssetPaymentSettingsNoSqlEntity> paymentsWriter,
            ILogger<AssetItemManager> logger)
        {
            _assetsDictionaryService = assetsDictionaryService;
            _brandAssetsAndInstrumentsService = brandAssetsAndInstrumentsService;
            _paymentsWriter = paymentsWriter;
            _logger = logger;
        }

        public async Task<List<AssetItem>> GetAll()
        {
            var assets = await _assetsDictionaryService.GetAllAssetsAsync();
            var payments = (await _paymentsWriter.GetAsync()).ToList();
            var brands = await _brandAssetsAndInstrumentsService.GetAllBrandMappingAsync();
            
            var result = new List<AssetItem>();
            foreach (var asset in assets.Assets)
            {
                var item = new AssetItem();
                
                var payment = payments
                    .FirstOrDefault(e => e.BrokerId == asset.BrokerId && e.Symbol == asset.Symbol);
                
                item.Asset = asset;
                item.PaymentSettings = payment ?? AssetPaymentSettingsNoSqlEntity.Create(asset,
                    new AssetPaymentSettings()
                    {
                        AssetSymbol = asset.Symbol,
                        PciDss = new AssetPaymentSettings.PciDssSettings()
                        {
                            IsEnabledDeposit = false,
                        },
                        BitGoCrypto = new AssetPaymentSettings.BitGoCryptoSettings()
                        {
                            IsEnabledDeposit = false,
                            IsEnabledWithdrawal = false
                        }
                    });

                item.Brands = brands.Brands?
                    .Where(e => e.BrokerId == asset.BrokerId && e.AssetSymbolsList?.Contains(asset.Symbol) == true)
                    .Select(e => e.BrandId)
                    .ToList() ?? new List<string>();
                
                result.Add(item);
            }

            return result;
        }

        public async Task CreateAsset(AssetItem item)
        {
            try
            {
                var resp = await _assetsDictionaryService.CreateAssetAsync(item.Asset);
                if (!resp.IsSuccess)
                    throw new Exception(resp.ErrorMessage);

                if (item.PaymentSettings != null)
                {
                    await _paymentsWriter.InsertOrReplaceAsync(item.PaymentSettings);
                }

                await AddBrand(item, "default-brand");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Cannot create AssetItem: {jsonText}", JsonConvert.SerializeObject(item));
                throw new Exception(ex.Message, ex);
            }
        }

        public async Task UpdateAsset(AssetItem item)
        {
            try
            {
                var resp = await _assetsDictionaryService.UpdateAssetAsync(item.Asset);
                
                if (!resp.IsSuccess)
                    throw new Exception(resp.ErrorMessage);

                if (item.PaymentSettings != null)
                {
                    await _paymentsWriter.InsertOrReplaceAsync(item.PaymentSettings);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Cannot update AssetItem: {jsonText}", JsonConvert.SerializeObject(item));
                throw new Exception(ex.Message, ex);
            }
        }

        public async Task DeleteAsset(AssetItem item)
        {
            try
            {
                var resp = await _assetsDictionaryService.DeleteAssetAsync(item.Asset);
                
                if (!resp.IsSuccess)
                    throw new Exception(resp.ErrorMessage);

                if (item.PaymentSettings != null)
                {
                    await _paymentsWriter.DeleteAsync(item.PaymentSettings.PartitionKey, item.PaymentSettings.RowKey);
                }

                foreach (var brand in item.Brands)
                {
                    await _brandAssetsAndInstrumentsService.RemoveAssetAsync(new AssetIdBrandIdRequest(
                        new AssetIdentity() {BrokerId = item.Asset.BrokerId, Symbol = item.Asset.Symbol},
                        new JetBrandIdentity(item.Asset.BrokerId, brand)));
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Cannot delete AssetItem: {jsonText}", JsonConvert.SerializeObject(item));
                throw new Exception(ex.Message, ex);
            }
        }

        public async Task AddBrand(AssetItem item, string brandId)
        {
            try
            {
                var resp = await _brandAssetsAndInstrumentsService.AddAssetAsync(
                    new AssetIdBrandIdRequest(
                        new AssetIdentity() {BrokerId = item.Asset.BrokerId, Symbol = item.Asset.Symbol},
                        new JetBrandIdentity(item.Asset.BrokerId, brandId)));

                if (resp.IsSuccess)
                {
                    item.Brands.Add(brandId);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Cannot add to brand AssetItem: {jsonText}", JsonConvert.SerializeObject(item));
                throw new Exception(ex.Message, ex);
            }
        }

        public async Task RemoveBrand(AssetItem item, string brandId)
        {
            try
            {
                var resp = await _brandAssetsAndInstrumentsService.RemoveAssetAsync(
                    new AssetIdBrandIdRequest(
                        new AssetIdentity() {BrokerId = item.Asset.BrokerId, Symbol = item.Asset.Symbol},
                        new JetBrandIdentity(item.Asset.BrokerId, brandId)));

                if (resp.IsSuccess)
                {
                    item.Brands.Remove(brandId);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Cannot remove from brand AssetItem: {jsonText}", JsonConvert.SerializeObject(item));
                throw new Exception(ex.Message, ex);
            }
        }
    }
}