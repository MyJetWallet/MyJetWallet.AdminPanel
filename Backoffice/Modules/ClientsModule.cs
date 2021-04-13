using Autofac;
using MyJetWallet.Sdk.Service;
using MyNoSqlServer.DataReader;
using Service.AssetsDictionary.Client.Grpc;
using Service.AssetsDictionary.Grpc;
using Service.ClientWallets.Client;
using Service.Liquidity.Engine.Client;
using Service.Liquidity.Reports.Client;
using Service.MatchingEngine.PriceSource.Client;
using Service.Simulation.FTX.Client;

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

            builder.RegisterClientWalletsClientsWithoutCache(Program.Settings.ClientWalletsGrpcServiceUrl);
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