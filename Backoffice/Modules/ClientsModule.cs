using Autofac;
using Backoffice.Services.ExternalMarkets;
using Backoffice.Services.Simulations;
using MyCrm.PersonalData.Grpc;
using MyJetWallet.BitGo.Settings.Ioc;
using MyJetWallet.Sdk.Grpc;
using MyJetWallet.Sdk.NoSql;
using MyJetWallet.Sdk.Service;
using MyNoSqlServer.DataReader;
using Service.ActiveOrders.Client;
using Service.AssetsDictionary.Client.Grpc;
using Service.AssetsDictionary.Grpc;
using Service.BalanceHistory.Client;
using Service.Balances.Client;
using Service.Bitgo.DepositDetector.Client;
using Service.Bitgo.WithdrawalProcessor.Client;
using Service.ChangeBalanceGateway.Client;
using Service.ClientWallets.Client;
using Service.CoinMarketCapReader.Client;
using Service.Fees.Client.Grpc;
using Service.IndexPrices.Client;
using Service.Liquidity.Converter.Client;
using Service.Liquidity.Engine.Client;
using Service.Liquidity.Monitoring.Client;
using Service.Liquidity.Monitoring.Domain.Models;
using Service.Liquidity.Portfolio.Client;
using Service.Liquidity.Portfolio.Domain.Models;
using Service.Liquidity.PortfolioHedger.Client;
using Service.Liquidity.Reports.Client;
using Service.MatchingEngine.PriceSource.Client;
using Service.MessageTemplates.Client;
using Service.NewsRepository.Client;
using Service.PriceHistory.Client;
using Service.PushNotification.Client;
using Service.Service.KYC.Client;
using Service.SmsProviderMock.Client;
using Service.SmsSender.Client;
using SimpleTrading.PersonalData.Grpc;

namespace Backoffice.Modules
{
    public class ClientsModule : Module
    {
        private MyNoSqlTcpClient _myNoSqlClient;

        protected override void Load(ContainerBuilder builder)
        {
            RegisterMyNoSqlTcpClient(builder);
            
            var assetDictionaryFactory = new AssetsDictionaryClientFactory(Program.Settings.AssetDictionaryGrpcServiceUrl);

            builder
                .RegisterInstance(assetDictionaryFactory.GetAssetsDictionaryService())
                .As<IAssetsDictionaryService>()
                .SingleInstance();

            builder
                .RegisterInstance(assetDictionaryFactory.GetSpotInstrumentsDictionaryService())
                .As<ISpotInstrumentsDictionaryService>()
                .SingleInstance();

            builder
                .RegisterInstance(assetDictionaryFactory.GetBrandAssetsAndInstrumentsService())
                .As<IBrandAssetsAndInstrumentsService>()
                .SingleInstance();
            
            builder
                .RegisterInstance(assetDictionaryFactory.GetMarketReferenceDictionaryService())
                .As<IMarketReferencesDictionaryService>()
                .SingleInstance();
            
            builder
                .RegisterInstance(assetDictionaryFactory.GetSpotConvertExternalInstrumentsDictionaryService())
                .As<ISpotConvertExternalInstrumentsDictionaryService>()
                .SingleInstance();

            builder.RegisterLiquidityEngineClient(Program.Settings.LiquidityEngineGrpcServiceUrl);

            var simulationManager = new SimulationsManager(Program.Settings.SimulationsFTX, Program.Settings.SimulationsBinance);
            builder.RegisterInstance(simulationManager).As<ISimulationsManager>().SingleInstance();

            builder.RegisterLiquidityReportClient(Program.Settings.LiquidityReportGrpcServiceUrl);
            builder.RegisterSmsSenderClient(Program.Settings.SmsSenderGrpcServiceUrl);
            builder.RegisterSmsSenderAdminClients(Program.Settings.SmsSenderGrpcServiceUrl);
            builder.RegisterSmsProviderMockClient(Program.Settings.SmsProviderMockGrpcServiceUrl);
            //builder.RegisterSmsProviderNexmoClient(Program.Settings.SmsProviderNexmoGrpcServiceUrl);
            //builder.RegisterSmsProviderTwilioClient(Program.Settings.SmsProviderTwilioGrpcServiceUrl);

            builder.RegisterLiquidityConverterManagerClient(Program.Settings.LiquidityConverterGrpcServiceUrl);

            builder.RegisterMatchingEnginePriceSourceClient(_myNoSqlClient);
            builder.RegisterMatchingEngineOrderBookClient(_myNoSqlClient);

            builder.RegisterPortfolioClient(Program.Settings.LiquidityPortfolioServiceUrl);
            builder.RegisterAssetPortfolioSettingsClient(Program.Settings.LiquidityPortfolioMonitoringServiceUrl);
            builder.RegisterPortfolioSimulationClient(Program.Settings.LiquidityPortfolioSimulationServiceUrl);
            
            builder.RegisterIndexPricesClient(_myNoSqlClient);
            builder.RegisterIndexPricesManager(Program.Settings.MyNoSqlWriterUrl);
            
            builder.RegisterConvertIndexPricesClient(_myNoSqlClient);
            builder.RegisterConvertIndexPricesManager(Program.Settings.MyNoSqlWriterUrl);
            

            builder.RegisterMyNoSqlReader<AssetPortfolioBalanceNoSql>(_myNoSqlClient, AssetPortfolioBalanceNoSql.TableName);
            builder.RegisterMyNoSqlReader<AssetPortfolioStatusNoSql>(_myNoSqlClient, AssetPortfolioStatusNoSql.TableName);
            builder.RegisterMyNoSqlReader<AssetPortfolioSettingsNoSql>(_myNoSqlClient, AssetPortfolioSettingsNoSql.TableName);


            builder.RegisterClientWalletsClientsWithoutCache(Program.Settings.ClientWalletsGrpcServiceUrl);
            
            builder.RegisterBalancesClientsWithoutCache(Program.Settings.BalancesGrpcServiceUrl);

            builder.RegisterSpotChangeBalanceGatewayClient(Program.Settings.ChangeBalanceGatewayGrpcServiceUrl);

            builder.RegisterBalanceHistoryClient(Program.Settings.BalanceHistoryGrpcServiceUrl);
            builder.RegisterSwapHistoryClient(Program.Settings.BalanceHistoryGrpcServiceUrl);

            builder.RegisterActiveOrdersClientsWithoutCache(Program.Settings.ActiveOrdersGrpcServiceUrl);

            builder.RegisterTradeHistoryClient(Program.Settings.BalanceHistoryGrpcServiceUrl);

            builder.RegisterKycStatusClientsGrpcOnly(Program.Settings.KycGrpcServiceUrl);

            builder.RegisterBitgoSettingsWriter(Program.ReloadedSettings(e => e.MyNoSqlWriterUrl));
            
            builder.RegisterPushNotificationClient(Program.Settings.PushNotificationGrpcServiceUrl);

            var personalDataFactory = new MyGrpcClientFactory(Program.Settings.PersonalDataServiceUrl);
            
            builder
                .RegisterInstance(personalDataFactory.CreateGrpcService<IPersonalDataServiceGrpc>())
                .As<IPersonalDataServiceGrpc>()
                .SingleInstance();
            
            var crmPersonalDataFactory = new MyGrpcClientFactory(Program.Settings.CrmPersonalDataServiceUrl);
            builder
                .RegisterInstance(crmPersonalDataFactory.CreateGrpcService<IMyCrmPersonalDataGrpcService>())
                .As<IMyCrmPersonalDataGrpcService>()
                .SingleInstance();
            
            var externalSettingsManager = new ExternalMarketsSettingsManager(Program.Settings.ExternalMarketsSettings);
            builder.RegisterInstance(externalSettingsManager).As<IExternalMarketsSettingsManager>().SingleInstance();
            
            builder.RegisterFeesSettingsClients(Program.Settings.FeesServiceUrl);
            
            builder.RegisterPortfolioHedgerClient(Program.Settings.PortfolioHedgerGrpcUrl);
            builder.RegisterExternalExchangeSettingsServiceClient(Program.Settings.PortfolioHedgerGrpcUrl);
            
            builder.RegisterBitgoDepositServiceClient(Program.Settings.BitgoDepositServiceGrpcUrl);
            builder.RegisterBitgoWithdrawalClient(Program.Settings.BitgoWithdrawalServiceGrpcUrl);
            
            builder.RegisterPriceHistoryClient(Program.Settings.BasePriceServiceGrpcUrl);
            builder.RegisterMessageTemplatesCachedClient(Program.Settings.MessageTemplatesGrpcServiceUrl, _myNoSqlClient);
            
            builder.RegisterNewsRepositoryClient(Program.Settings.NewsRepositoryGrpcServiceUrl);
        }
        
        private void RegisterMyNoSqlTcpClient(ContainerBuilder builder)
        {
            _myNoSqlClient = new MyNoSqlTcpClient(Program.ReloadedSettings(e => e.MyNoSqlReaderHostPort),
                ApplicationEnvironment.HostName ?? $"{ApplicationEnvironment.AppName}:{ApplicationEnvironment.AppVersion}");

            builder
                .RegisterInstance(_myNoSqlClient)
                .AsSelf()
                .SingleInstance();
            }
    }
}