using System.Collections.Generic;
using MyYamlParser;

namespace Backoffice
{
    public class SettingsModel
    {
        [YamlProperty("AdminPanel.TableStorageConnectionString")]
        public string TableStorageConnectionString { get; set; }

        [YamlProperty("AdminPanel.TokenKey")] 
        public string TokenKey { get; set; }
        
        [YamlProperty("AdminPanel.SeqServiceUrl")]
        public string SeqServiceUrl { get; set; }

        [YamlProperty("AdminPanel.MyNoSqlWriterUrl")]
        public string MyNoSqlWriterUrl { get; set; }
        
        [YamlProperty("AdminPanel.MyNoSqlReaderHostPort")]
        public string MyNoSqlReaderHostPort { get; set; }

        [YamlProperty("AdminPanel.AssetDictionaryGrpcServiceUrl")]
        public string AssetDictionaryGrpcServiceUrl { get; set; }
        
        [YamlProperty("AdminPanel.LiquidityEngineGrpcServiceUrl")]
        public string LiquidityEngineGrpcServiceUrl { get; set; }

        [YamlProperty("AdminPanel.LiquidityReportGrpcServiceUrl")]
        public string LiquidityReportGrpcServiceUrl { get; set; }
        
        [YamlProperty("AdminPanel.SmsSenderGrpcServiceUrl")]
        public string SmsSenderGrpcServiceUrl { get; set; }
        
        [YamlProperty("AdminPanel.SmsProviderMockGrpcServiceUrl")]
        public string SmsProviderMockGrpcServiceUrl { get; set; }

        [YamlProperty("AdminPanel.ZipkinUrl")]
        public string ZipkinUrl { get; set; }

        [YamlProperty("AdminPanel.ClientWalletsGrpcServiceUrl")]
        public string ClientWalletsGrpcServiceUrl { get; set; }
        
        [YamlProperty("AdminPanel.BalancesGrpcServiceUrl")]
        public string BalancesGrpcServiceUrl { get; set; }
        
        [YamlProperty("AdminPanel.ChangeBalanceGatewayGrpcServiceUrl")]
        public string ChangeBalanceGatewayGrpcServiceUrl { get; set; }

        [YamlProperty("AdminPanel.BalanceHistoryGrpcServiceUrl")]
        public string BalanceHistoryGrpcServiceUrl { get; set; }

        [YamlProperty("AdminPanel.ActiveOrdersGrpcServiceUrl")]
        public string ActiveOrdersGrpcServiceUrl { get; set; }
        
        [YamlProperty("AdminPanel.TradeHistoryGrpcServiceUrl")]
        public string TradeHistoryGrpcServiceUrl { get; set; }

        [YamlProperty("AdminPanel.KycGrpcServiceUrl")]
        public string KycGrpcServiceUrl { get; set; }
        
        [YamlProperty("AdminPanel.Simulations")]
        public Dictionary<string, string> Simulations { get; set; }

        [YamlProperty("AdminPanel.LiquidityConverterGrpcServiceUrl")]
        public string LiquidityConverterGrpcServiceUrl { get; set; }
    }
}