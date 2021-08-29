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
        
        [YamlProperty("AdminPanel.KycGrpcServiceUrl")]
        public string KycGrpcServiceUrl { get; set; }
        
        [YamlProperty("AdminPanel.SimulationsFTX")]
        public Dictionary<string, string> SimulationsFTX { get; set; }
        
        [YamlProperty("AdminPanel.SimulationsBinance")]
        public Dictionary<string, string> SimulationsBinance { get; set; }
        
        [YamlProperty("AdminPanel.ExternalMarketsSettings")]
        public Dictionary<string, string> ExternalMarketsSettings { get; set; }

        [YamlProperty("AdminPanel.LiquidityConverterGrpcServiceUrl")]
        public string LiquidityConverterGrpcServiceUrl { get; set; }
        
        [YamlProperty("AdminPanel.PushNotificationGrpcServiceUrl")]
        public string PushNotificationGrpcServiceUrl { get; set; }
        
        [YamlProperty("AdminPanel.AuthMyNoSqlReaderHostPort")]
        public string AuthMyNoSqlReaderHostPort { get; set; }
        
        [YamlProperty("AdminPanel.AuthMyNoSqlWriterUrl")]
        public string AuthMyNoSqlWriterUrl { get; set; }

        [YamlProperty("AdminPanel.PersonalDataServiceUrl")]
        public string PersonalDataServiceUrl { get; set; }
        
        [YamlProperty("AdminPanel.CrmPersonalDataServiceUrl")]
        public string CrmPersonalDataServiceUrl { get; set; }

        [YamlProperty("AdminPanel.LiquidityPortfolioServiceUrl")]
        public string LiquidityPortfolioServiceUrl { get; set; }

        [YamlProperty("AdminPanel.FeesServiceUrl")]
        public string FeesServiceUrl { get; set; }

        [YamlProperty("AdminPanel.FeesWalletId")]
        public string FeesWalletId { get; set; }
              
        [YamlProperty("AdminPanel.GoogleClientId")]
        public string GoogleClientId { get; set; }
        
        [YamlProperty("AdminPanel.FacebookClientId")]
        public string FacebookClientId { get; set; }

        [YamlProperty("AdminPanel.PortfolioHedgerGrpcUrl")]
        public string PortfolioHedgerGrpcUrl { get; set; }

        [YamlProperty("AdminPanel.BitgoDepositServiceGrpcUrl")]
        public string BitgoDepositServiceGrpcUrl { get; set; }

        [YamlProperty("AdminPanel.BitgoWithdrawalServiceGrpcUrl")]
        public string BitgoWithdrawalServiceGrpcUrl { get; set; }
        
        [YamlProperty("AdminPanel.BitGoSignTransactionGrpcServiceUrl")]
        public string BitGoSignTransactionGrpcServiceUrl { get; set; }

        [YamlProperty("AdminPanel.LiquidityPortfolioMonitoringServiceUrl")]
        public string LiquidityPortfolioMonitoringServiceUrl { get; set; }
        
        [YamlProperty("AdminPanel.BasePriceServiceGrpcUrl")]
        public string BasePriceServiceGrpcUrl { get; set; }

        [YamlProperty("AdminPanel.MessageTemplatesGrpcServiceUrl")]
        public string MessageTemplatesGrpcServiceUrl { get; set; }

        [YamlProperty("AdminPanel.LiquidityPortfolioSimulationServiceUrl")]
        public string LiquidityPortfolioSimulationServiceUrl { get; set; }
        
        [YamlProperty("AdminPanel.NewsRepositoryGrpcServiceUrl")]
        public string NewsRepositoryGrpcServiceUrl { get; set; }
        
        [YamlProperty("AdminPanel.ExternalApiGrpcUrl")]
        public string ExternalApiGrpcUrl { get; set; }

        [YamlProperty("AdminPanel.WalletObserverGrpcUrl")]
        public string WalletObserverGrpcUrl { get; set; }
        
        [YamlProperty("AdminPanel.CrmUserId")]
        public string CrmUserId { get; set; }

        [YamlProperty("AdminPanel.InternalWalletsGrpcUrl")]
        public string InternalWalletsGrpcUrl { get; set; }

        [YamlProperty("AdminPanel.CandlesServiceGrpcUrl")]
        public string CandlesServiceGrpcUrl { get; set; }
        
        [YamlProperty("AdminPanel.ActivityObserverServiceGrpcUrl")]
        public string ActivityObserverServiceGrpcUrl { get; set; }
    }
}