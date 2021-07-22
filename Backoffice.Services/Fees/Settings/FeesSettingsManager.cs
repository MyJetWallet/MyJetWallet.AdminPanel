using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Service.Fees.Domain.Models;
using Service.Fees.Grpc;
using Service.Fees.Grpc.Models;

namespace Backoffice.Services.Fees.Settings
{
    public class FeesSettingsManager: IFeesSettingsManager
    {
        private readonly IFeesSettingsService _feesSettingsService;
        private readonly ILogger<FeesSettingsManager> _logger;

        public FeesSettingsManager(IFeesSettingsService feesSettingsService, ILogger<FeesSettingsManager> logger)
        {
            _feesSettingsService = feesSettingsService;
            _logger = logger;
        }

        public async Task<List<FeesSettings>> GetAll()
        {
            return (await _feesSettingsService.GetFeesSettingsList()).OrderBy(e => e.BrokerId).ToList();
        }

        public async Task CreateFeesSettings(FeesSettings item)
        {
            try
            {
                _logger.LogInformation("Creating fees settings: {jsonText}", JsonConvert.SerializeObject(item));
                await _feesSettingsService.AddFeesSettings(item);
                _logger.LogInformation("Created fees settings: {jsonText}", JsonConvert.SerializeObject(item));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Cannot create fees settings: {jsonText}", JsonConvert.SerializeObject(item));
                throw new Exception(ex.Message, ex);
            }
        }

        public async Task UpdateFeesSettings(FeesSettings item)
        {
            try
            {
                _logger.LogInformation("Updating fees settings: {jsonText}", JsonConvert.SerializeObject(item));
                await _feesSettingsService.UpdateFeesSettings(item);
                _logger.LogInformation("Updated fees settings: {jsonText}", JsonConvert.SerializeObject(item));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Cannot asset fees settings: {jsonText}", JsonConvert.SerializeObject(item));
                throw new Exception(ex.Message, ex);
            }
        }

        public async Task DeleteFeesSettings(FeesSettings item)
        {
            try
            {
                _logger.LogInformation("Deleting fees settings: {jsonText}", JsonConvert.SerializeObject(item));
                await _feesSettingsService.RemoveFeesSettings(new RemoveFeesSettingsRequest()
                {
                    BrokerId = item.BrokerId
                });
                _logger.LogInformation("Deleted fees settings: {jsonText}", JsonConvert.SerializeObject(item));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Cannot delete fees settings: {jsonText}", JsonConvert.SerializeObject(item));
                throw new Exception(ex.Message, ex);
            }
        }
    }
}