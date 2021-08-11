using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backoffice.Services.SpotInstruments;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Service.AssetsDictionary.Domain.Models;
using Service.AssetsDictionary.Grpc;

namespace Backoffice.Services.SpotExternalInstruments
{
    public class SpotExternalInstrumentManager : ISpotExternalInstrumentManager
    {
        private readonly ILogger<SpotInstrumentManager> _logger;
        private readonly ISpotConvertExternalInstrumentsDictionaryService _instrumentService;
        private readonly IBrandAssetsAndInstrumentsService _brandAssetsAndInstrumentsService;

        public SpotExternalInstrumentManager(ILogger<SpotInstrumentManager> logger,
            ISpotConvertExternalInstrumentsDictionaryService instrumentService,
            IBrandAssetsAndInstrumentsService brandAssetsAndInstrumentsService)
        {
            _logger = logger;
            _instrumentService = instrumentService;
            _brandAssetsAndInstrumentsService = brandAssetsAndInstrumentsService;
        }

        public async Task<List<SpotConvertExternalInstrument>> GetAll()
        {
            var instruments = await _instrumentService.GetAllSpotInstrumentsAsync();
            return instruments.SpotConvertExternalInstruments.ToList();
        }

        public async Task AddInstrument(SpotConvertExternalInstrument item)
        {
            try
            {
                var resp = await _instrumentService.CreateSpotConvertExternalInstrumentAsync(item);
                
                if (!resp.IsSuccess)
                    throw new Exception(resp.ErrorMessage);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Cannot create SpotInstrument: {jsonText}", JsonConvert.SerializeObject(item));
                throw new Exception(ex.Message, ex);
            }
        }

        public async Task UpdateInstrument(SpotConvertExternalInstrument item)
        {
            try
            {
                var resp = await _instrumentService.UpdateSpotConvertExternalInstrumentAsync(item);
                if (!resp.IsSuccess)
                    throw new Exception(resp.ErrorMessage);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Cannot update SpotInstrument: {jsonText}", JsonConvert.SerializeObject(item));
                throw new Exception(ex.Message, ex);
            }
        }

        public async Task DeleteInstrument(SpotConvertExternalInstrument item)
        {
            try
            {
                await _instrumentService.DeleteSpotConvertExternalInstrumentAsync(item);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Cannot delete SpotInstrument: {jsonText}", JsonConvert.SerializeObject(item));
                throw new Exception(ex.Message, ex);
            }
        }
    }
}