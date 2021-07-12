using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backoffice.Services.SpotInstruments;
using Microsoft.Extensions.Logging;
using MyJetWallet.Domain;
using MyJetWallet.Domain.Assets;
using Newtonsoft.Json;
using Service.AssetsDictionary.Grpc;
using Service.AssetsDictionary.Grpc.Models;

namespace Backoffice.Services.References
{
    public class MarketReferenceManager : IMarketReferenceManager
    {
        private readonly ILogger<MarketReferenceManager> _logger;
        private readonly IMarketReferencesDictionaryService _referencesService;
        private readonly IBrandAssetsAndInstrumentsService _brandAssetsAndInstrumentsService;

        public MarketReferenceManager(ILogger<MarketReferenceManager> logger,
            IMarketReferencesDictionaryService referencesService,
            IBrandAssetsAndInstrumentsService brandAssetsAndInstrumentsService)
        {
            _logger = logger;
            _referencesService = referencesService;
            _brandAssetsAndInstrumentsService = brandAssetsAndInstrumentsService;
        }

        public async Task<List<MarketReferenceItem>> GetAll()
        {
            var references = await _referencesService.GetAllMarketReferencesAsync();
            var brands = await _brandAssetsAndInstrumentsService.GetAllBrandMappingAsync();

            var list = new List<MarketReferenceItem>();

            foreach (var reference in references.References)
            {
                var item = new MarketReferenceItem();
                item.Reference = reference;

                item.Brands = brands.Brands
                    .Where(e => e.BrokerId == reference.BrokerId
                                && e.MarketReferenceIdsList?.Contains(reference.Id) == true)
                    .Select(e => e.BrandId)
                    .ToList();
                
                list.Add(item);
            }

            return list;
        }

        public async Task AddReference(MarketReferenceItem item)
        {
            try
            {
                item.Brands ??= new List<string>();
                
                var resp = await _referencesService.CreateMarketReferenceAsync(item.Reference);
                
                if (!resp.IsSuccess)
                    throw new Exception(resp.ErrorMessage);

                await AddBrand(item,"default-brand");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Cannot create MarketReference: {jsonText}", JsonConvert.SerializeObject(item));
                throw new Exception(ex.Message, ex);
            }        
        }

        public async Task UpdateReference(MarketReferenceItem item)
        {
            try
            {
                item.Brands ??= new List<string>();
                
                var resp = await _referencesService.UpdateMarketReferenceAsync(item.Reference);
                
                if (!resp.IsSuccess)
                    throw new Exception(resp.ErrorMessage);
                
                
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Cannot update MarketReference: {jsonText}", JsonConvert.SerializeObject(item));
                throw new Exception(ex.Message, ex);
            }
        }

        public async Task DeleteReference(MarketReferenceItem item)
        {
            try
            {
                await _referencesService.DeleteMarketReferenceAsync(item.Reference);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Cannot delete MarketReference: {jsonText}", JsonConvert.SerializeObject(item));
                throw new Exception(ex.Message, ex);
            }
        }

        public async Task AddBrand(MarketReferenceItem item, string brand)
        {
            try
            {
                if (item.Brands.Contains(brand))
                    return;
                
                var res = await _brandAssetsAndInstrumentsService.AddMarketReferenceAsync(
                    new MarketReferenceIdBrandIdRequest(
                        new MarketReferenceIdentity()
                            {BrokerId = item.Reference.BrokerId, Id = item.Reference.Id},
                        new JetBrandIdentity(item.Reference.BrokerId, brand)));

                if (!res.IsSuccess || !res.Data)
                    throw new Exception("Cannot add reference to brand. Error on server side");
                
                item.Brands.Add(brand);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Cannot add MarketReference: {jsonText}; to brand: {brandId}", 
                    JsonConvert.SerializeObject(item), brand);
                throw new Exception(ex.Message, ex);
            }        
        }

        public async Task RemoveBrand(MarketReferenceItem item, string brand)
        {
            try
            {
                if (!item.Brands.Contains(brand))
                    return;
                
                var res = await _brandAssetsAndInstrumentsService.RemoveMarketReferenceAsync(
                    new MarketReferenceIdBrandIdRequest(
                        new MarketReferenceIdentity()
                            {BrokerId = item.Reference.BrokerId, Id = item.Reference.Id},
                        new JetBrandIdentity(item.Reference.BrokerId, brand)));

                if (!res.IsSuccess || !res.Data)
                    throw new Exception("Cannot remove reference from brand. Error on server side");
                
                item.Brands.Remove(brand);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Cannot remove MarketReference: {jsonText}; from brand: {brandId}", 
                    JsonConvert.SerializeObject(item), brand);
            }
            
        }
    }
}