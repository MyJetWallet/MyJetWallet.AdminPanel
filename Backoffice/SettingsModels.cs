using MyYamlParser;

namespace Backoffice
{
    public class SettingsModel
    {
        [YamlProperty("AdminPanel.AccountsGrpcHostPort")]
        public string AccountsGrpcHostPort { get; set; }

        [YamlProperty("AdminPanel.TransactionsGrpcHostPort")]
        public string TransactionsGrpcHostPort { get; set; }

        [YamlProperty("AdminPanel.PersonalDataGrpcHostPort")]
        public string PersonalDataGrpcHostPort { get; set; }

        [YamlProperty("AdminPanel.ActiveDealsGrpcService")]
        public string ActiveDealsGrpcService { get; set; }
        
        [YamlProperty("AdminPanel.DepositManagerGrpcService")]
        public string DepositManagerGrpcService { get; set; }

        [YamlProperty("AdminPanel.LogsGrpcUrl")] 
        public string LogsGrpcUrl { get; set; }

        [YamlProperty("AdminPanel.KycGrpcUrl")] 
        public string KycGrpcUrl { get; set; }

        [YamlProperty("AdminPanel.StatusesGrpcUrl")] 
        public string StatusesGrpcUrl { get; set; }

        [YamlProperty("AdminPanel.AuthGrpcServiceUrl")]
        public string AuthGrpcServiceUrl { get; set; }

        [YamlProperty("AdminPanel.PhonePoolServiceUrl")]
        public string PhonePoolServiceUrl { get; set; }

        [YamlProperty("AdminPanel.TableStorageConnectionString")]
        public string TableStorageConnectionString { get; set; }

        [YamlProperty("AdminPanel.TokenKey")] 
        public string TokenKey { get; set; }
        
        [YamlProperty("AdminPanel.MonfexApi")] 
        public string MonfexApi { get; set; }

        [YamlProperty("AdminPanel.AlianceApi")] 
        public string AlianceApi { get; set; }

        [YamlProperty("AdminPanel.HpApi")] 
        public string HpApi { get; set; }

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

        [YamlProperty("AdminPanel.SimulationFtxGrpcServiceUrl")]
        public string SimulationFtxGrpcServiceUrl { get; set; }

        [YamlProperty("AdminPanel.LiquidityReportGrpcServiceUrl")]
        public string LiquidityReportGrpcServiceUrl { get; set; }

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
    }
}