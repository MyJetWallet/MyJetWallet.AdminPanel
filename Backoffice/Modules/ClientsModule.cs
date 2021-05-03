using Autofac;
using Google.Protobuf;
using MyJetWallet.Sdk.Service;
using MyNoSqlServer.DataReader;
using Service.ActiveOrders.Client;
using Service.ActiveOrders.Grpc;
using Service.AssetsDictionary.Client.Grpc;
using Service.AssetsDictionary.Grpc;
using Service.BalanceHistory.Client;
using Service.Balances.Client;
using Service.ChangeBalanceGateway.Client;
using Service.ClientWallets.Client;
using Service.Liquidity.Engine.Client;
using Service.Liquidity.Reports.Client;
using Service.MatchingEngine.PriceSource.Client;
using Service.Service.KYC.Client;
using Service.Simulation.FTX.Client;
using Service.TradeHistory.Client;

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

            builder.RegisterLiquidityEngineClient(Program.Settings.LiquidityEngineGrpcServiceUrl);

            builder.RegisterSimulationFtxClient(Program.Settings.SimulationFtxGrpcServiceUrl);

            builder.RegisterLiquidityReportClient(Program.Settings.LiquidityReportGrpcServiceUrl);
            
            
            builder.RegisterMatchingEnginePriceSourceClient(_myNoSqlClient);
            builder.RegisterMatchingEngineOrderBookClient(_myNoSqlClient);
            builder.RegisterMatchingEngineDetailOrderBookClient(_myNoSqlClient);
            
            builder.RegisterClientWalletsClientsWithoutCache(Program.Settings.ClientWalletsGrpcServiceUrl);
            
            builder.RegisterBalancesClientsWithoutCache(Program.Settings.BalancesGrpcServiceUrl);

            builder.RegisterSpotChangeBalanceGatewayClient(Program.Settings.ChangeBalanceGatewayGrpcServiceUrl);

            builder.RegisterBalanceHistoryClient(Program.Settings.BalanceHistoryGrpcServiceUrl);

            var activeOrderClientFactory = new ActiveOrdersClientFactory(Program.Settings.ActiveOrdersGrpcServiceUrl, null);
            builder
                .RegisterInstance(activeOrderClientFactory.ActiveOrderServiceGrpc())
                .As<IActiveOrderService>()
                .SingleInstance();

            builder.RegisterTradeHistoryClient(Program.Settings.TradeHistoryGrpcServiceUrl);

            builder.RegisterKycStatusClientsGrpcOnly(Program.Settings.KycGrpcServiceUrl);
            
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