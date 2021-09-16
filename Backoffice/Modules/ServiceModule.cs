using Allegiance.Blazor.Highcharts.Services;
using Autofac;
using Backoffice.Abstractions.Bo;
using Backoffice.Services;
using Backoffice.Services.Assets;
using Backoffice.Services.Backoffice;
using Backoffice.Services.BitGo.Assets;
using Backoffice.Services.BitGo.Coins;
using Backoffice.Services.Fees.Assets;
using Backoffice.Services.Fees.Instruments;
using Backoffice.Services.Fees.Settings;
using Backoffice.Services.References;
using Backoffice.Services.SpotExternalInstruments;
using Backoffice.Services.SpotInstruments;
using Backoffice.TableStorage;
using Backoffice.Users;

namespace Backoffice.Modules
{
    public class ServiceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterType<AssetItemManager>()
                .As<IAssetItemManager>()
                .SingleInstance();

            builder
                .RegisterType<SpotInstrumentManager>()
                .As<ISpotInstrumentManager>()
                .SingleInstance();
            
            builder
                .RegisterType<SpotExternalInstrumentManager>()
                .As<ISpotExternalInstrumentManager>()
                .SingleInstance();
            
            builder
                .RegisterType<MarketReferenceManager>()
                .As<IMarketReferenceManager>()
                .SingleInstance();
            
            builder
                .RegisterType<BitGoAssetManager>()
                .As<IBitGoAssetManager>()
                .SingleInstance();
            
            builder
                .RegisterType<BitGoCoinManager>()
                .As<IBitGoCoinManager>()
                .SingleInstance();
            
            builder
                .RegisterType<AssetFeesManager>()
                .As<IAssetFeesManager>()
                .SingleInstance();
            
            builder
                .RegisterType<InstrumentFeesManager>()
                .As<IInstrumentFeesManager>()
                .SingleInstance();
            
            builder
                .RegisterType<FeesSettingsManager>()
                .As<IFeesSettingsManager>()
                .SingleInstance();

            builder
                .RegisterType<BoUserAccessor>()
                .AsSelf()
                .As<IBoUserAccessor>()
                .InstancePerLifetimeScope();

            RegisterGrpcService(builder);
            RegisterTableStorage(builder);
            RegisterServices(builder);
            
            
        }

        private void RegisterServices(ContainerBuilder builder)
        {
            builder.RegisterType<LastSearchUserCache>().As<ILastSearchUserCache>().SingleInstance();

            builder.RegisterType<BoUsersService>().As<IBoUsersService>().SingleInstance();
            
            builder.RegisterType<ChartService>().As<IChartService>();
        }

        private static void RegisterTableStorage(ContainerBuilder builder)
        {
            builder
                .RegisterInstance(Program.Settings.TableStorageConnectionString.CreateRolesRepository())
                .As<IBackofficeRolesRepository>()
                .As<IStartable>()
                .AutoActivate()
                .SingleInstance();

        }

        private static void RegisterGrpcService(ContainerBuilder builder)
        {


            
        }
    }
}