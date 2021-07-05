using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Service.External.B2C2.Domain.Models.Settings;
using Service.External.B2C2.Grpc;
using Service.External.B2C2.Grpc.Models;

// ReSharper disable InconsistentLogPropertyNaming

namespace Backoffice.Services.ExternalMarkets.B2C2
{
    public class ExternalMarketsB2C2Manager : IExternalMarketsB2C2Manager
    {
        private readonly IExternalMarketSettingsManagerGrpc _externalMarketSettingsManagerGrpc;
        private readonly ILogger<ExternalMarketsB2C2Manager> _logger;

        public ExternalMarketsB2C2Manager(IExternalMarketSettingsManagerGrpc externalMarketSettingsManagerGrpc,
            ILogger<ExternalMarketsB2C2Manager> logger)
        {
            _externalMarketSettingsManagerGrpc = externalMarketSettingsManagerGrpc;
            _logger = logger;
        }

        public async Task<List<ExternalMarketSettings>> GetAll()
        {
            return (await _externalMarketSettingsManagerGrpc.GetExternalMarketSettingsList()).List;
        }

        public async Task CreateExternalMarketSettings(ExternalMarketSettings item)
        {
            try
            {
                _logger.LogInformation("Creating B2C2 ExternalMarketSettings: {jsonText}",
                    JsonConvert.SerializeObject(item));
                await _externalMarketSettingsManagerGrpc.AddExternalMarketSettings(item);
                _logger.LogInformation("Created B2C2 ExternalMarketSettings: {jsonText}",
                    JsonConvert.SerializeObject(item));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Cannot create B2C2 ExternalMarketSettings: {jsonText}",
                    JsonConvert.SerializeObject(item));
                throw new Exception(ex.Message, ex);
            }
        }

        public async Task UpdateExternalMarketSettings(ExternalMarketSettings item)
        {
            try
            {
                _logger.LogInformation("Updating B2C2 ExternalMarketSettings: {jsonText}",
                    JsonConvert.SerializeObject(item));
                await _externalMarketSettingsManagerGrpc.UpdateExternalMarketSettings(item);
                _logger.LogInformation("Updated B2C2 ExternalMarketSettings: {jsonText}",
                    JsonConvert.SerializeObject(item));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Cannot update B2C2 ExternalMarketSettings: {jsonText}",
                    JsonConvert.SerializeObject(item));
                throw new Exception(ex.Message, ex);
            }
        }

        public async Task DeleteExternalMarketSettings(ExternalMarketSettings item)
        {
            try
            {
                _logger.LogInformation("Deleting B2C2 ExternalMarketSettings: {jsonText}",
                    JsonConvert.SerializeObject(item));
                await _externalMarketSettingsManagerGrpc.RemoveExternalMarketSettings(new RemoveMarketRequest
                    {Symbol = item.Market});
                _logger.LogInformation("Deleted B2C2 ExternalMarketSettings: {jsonText}",
                    JsonConvert.SerializeObject(item));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Cannot delete B2C2 ExternalMarketSettings: {jsonText}",
                    JsonConvert.SerializeObject(item));
                throw new Exception(ex.Message, ex);
            }
        }
    }
}