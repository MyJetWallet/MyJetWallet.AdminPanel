using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MyJetWallet.Domain;
using MyJetWallet.Domain.Assets;
using Newtonsoft.Json;
using Service.AssetsDictionary.Grpc;
using Service.AssetsDictionary.Grpc.Models;

namespace Backoffice.Services.SpotInstruments
{
    public class SpotInstrumentManager : ISpotInstrumentManager
    {
        private readonly ILogger<SpotInstrumentManager> _logger;
        private readonly ISpotInstrumentsDictionaryService _instrumentService;
        private readonly IBrandAssetsAndInstrumentsService _brandAssetsAndInstrumentsService;

        public SpotInstrumentManager(ILogger<SpotInstrumentManager> logger,
            ISpotInstrumentsDictionaryService instrumentService,
            IBrandAssetsAndInstrumentsService brandAssetsAndInstrumentsService)
        {
            _logger = logger;
            _instrumentService = instrumentService;
            _brandAssetsAndInstrumentsService = brandAssetsAndInstrumentsService;
        }

        public async Task<List<SpotInstrumentItem>> GetAll()
        {
            var instruments = await _instrumentService.GetAllSpotInstrumentsAsync();
            var brands = await _brandAssetsAndInstrumentsService.GetAllBrandMappingAsync();

            var list = new List<SpotInstrumentItem>();

            foreach (var instrument in instruments.SpotInstruments)
            {
                var item = new SpotInstrumentItem();
                item.Instrument = instrument;

                item.Brands = brands.Brands
                    .Where(e => e.BrokerId == instrument.BrokerId
                                && e.SpotInstrumentSymbolsList?.Contains(instrument.Symbol) == true)
                    .Select(e => e.BrandId)
                    .ToList();
                
                list.Add(item);
            }

            return list;
        }

        public async Task AddInstrument(SpotInstrumentItem item)
        {
            try
            {
                item.Brands ??= new List<string>();
                
                var resp = await _instrumentService.CreateSpotInstrumentAsync(item.Instrument);
                
                if (!resp.IsSuccess)
                    throw new Exception(resp.ErrorMessage);

                await AddBrand(item,"default-brand");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Cannot create SpotInstrument: {jsonText}", JsonConvert.SerializeObject(item));
                throw new Exception(ex.Message, ex);
            }
        }

        public async Task UpdateInstrument(SpotInstrumentItem item)
        {
            try
            {
                item.Brands ??= new List<string>();
                
                var resp = await _instrumentService.UpdateSpotInstrumentAsync(item.Instrument);
                
                if (!resp.IsSuccess)
                    throw new Exception(resp.ErrorMessage);
                
                
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Cannot update SpotInstrument: {jsonText}", JsonConvert.SerializeObject(item));
                throw new Exception(ex.Message, ex);
            }
        }

        public async Task DeleteInstrument(SpotInstrumentItem item)
        {
            try
            {
                await _instrumentService.DeleteSpotInstrumentAsync(item.Instrument);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Cannot delete SpotInstrument: {jsonText}", JsonConvert.SerializeObject(item));
                throw new Exception(ex.Message, ex);
            }
        }

        public async Task AddBrand(SpotInstrumentItem item, string brand)
        {
            try
            {
                if (item.Brands.Contains(brand))
                    return;
                
                var res = await _brandAssetsAndInstrumentsService.AddSpotInstrumentAsync(
                    new SpotInstrumentIdBrandIdRequest(
                        new SpotInstrumentIdentity()
                            {BrokerId = item.Instrument.BrokerId, Symbol = item.Instrument.Symbol},
                        new JetBrandIdentity(item.Instrument.BrokerId, brand)));

                if (!res.IsSuccess || !res.Data)
                    throw new Exception("Cannot add instrument to brand. Error on server side");
                
                item.Brands.Add(brand);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Cannot add SpotInstrument: {jsonText}; to brand: {brandId}", 
                    JsonConvert.SerializeObject(item), brand);
                throw new Exception(ex.Message, ex);
            }
        }

        public async Task RemoveBrand(SpotInstrumentItem item, string brand)
        {
            try
            {
                if (!item.Brands.Contains(brand))
                    return;
                
                var res = await _brandAssetsAndInstrumentsService.RemoveSpotInstrumentAsync(
                    new SpotInstrumentIdBrandIdRequest(
                        new SpotInstrumentIdentity()
                            {BrokerId = item.Instrument.BrokerId, Symbol = item.Instrument.Symbol},
                        new JetBrandIdentity(item.Instrument.BrokerId, brand)));

                if (!res.IsSuccess || !res.Data)
                    throw new Exception("Cannot remove instrument from brand. Error on server side");
                
                item.Brands.Remove(brand);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Cannot remove SpotInstrument: {jsonText}; from brand: {brandId}", 
                    JsonConvert.SerializeObject(item), brand);
            }
        }
    }
}