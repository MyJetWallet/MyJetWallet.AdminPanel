using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Service.Fees.Domain.Models;
using Service.Fees.Grpc;
using Service.Fees.Grpc.Models;

// ReSharper disable InconsistentLogPropertyNaming

namespace Backoffice.Services.Fees.Instruments
{
    public class InstrumentFeesManager : IInstrumentFeesManager
    {
        private readonly ISpotInstrumentFeesSettingsService _spotInstrumentFeesService;
        private readonly ILogger<InstrumentFeesManager> _logger;

        public InstrumentFeesManager(
            ISpotInstrumentFeesSettingsService spotInstrumentFeesService,
            ILogger<InstrumentFeesManager> logger)
        {
            _spotInstrumentFeesService = spotInstrumentFeesService;
            _logger = logger;
        }

        public async Task<List<SpotInstrumentFees>> GetAll()
        {
            return (await _spotInstrumentFeesService.GetSpotInstrumentFeesSettingsList()).OrderBy(e => e.BrokerId).ToList();
        }

        public async Task CreateSpotInstrumentFeesSettings(SpotInstrumentFees item)
        {
            try
            {
                _logger.LogInformation("Creating spot instrument fees settings: {jsonText}", JsonConvert.SerializeObject(item));
                await _spotInstrumentFeesService.AddSpotInstrumentFeesSettings(item);
                _logger.LogInformation("Created spot instrument fees settings: {jsonText}", JsonConvert.SerializeObject(item));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Cannot create spot instrument fees settings: {jsonText}", JsonConvert.SerializeObject(item));
                throw new Exception(ex.Message, ex);
            }
        }

        public async Task UpdateSpotInstrumentFeesSettings(SpotInstrumentFees item)
        {
            try
            {
                _logger.LogInformation("Updating spot instrument fees settings: {jsonText}", JsonConvert.SerializeObject(item));
                await _spotInstrumentFeesService.UpdateSpotInstrumentFeesSettings(item);
                _logger.LogInformation("Updated spot instrument fees settings: {jsonText}", JsonConvert.SerializeObject(item));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Cannot update spot instrument fees settings: {jsonText}", JsonConvert.SerializeObject(item));
                throw new Exception(ex.Message, ex);
            }
        }

        public async Task DeleteSpotInstrumentFeesSettings(SpotInstrumentFees item)
        {
            try
            {
                _logger.LogInformation("Deleting spot instrument fees settings: {jsonText}", JsonConvert.SerializeObject(item));
                await _spotInstrumentFeesService.RemoveSpotInstrumentFeesSettings(new RemoveSpotInstrumentFeesRequest
                {
                    BrokerId = item.BrokerId,
                    SpotInstrumentId = item.SpotInstrumentId
                });
                _logger.LogInformation("Deleted spot instrument fees settings: {jsonText}", JsonConvert.SerializeObject(item));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Cannot delete spot instrument fees settings: {jsonText}", JsonConvert.SerializeObject(item));
                throw new Exception(ex.Message, ex);
            }
        }
    }
}