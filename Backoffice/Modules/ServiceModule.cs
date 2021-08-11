using Allegiance.Blazor.Highcharts.Services;
using Autofac;
using Backoffice.Abstractions.Bo;
using Backoffice.Mocks;
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
using MyCRM.AccountTransactions.Grpc;
using MyCrm.AffiliateAccess.Grpc;
using MyCrm.AuditLog.Grpc;
using MyCrm.Auth.GrpcContracts;
using MyCrm.AutoOwnerProfiles.Grpc;
using MyCrm.BusinessCategories.Grpc;
using MyCrm.Calls.Grpc;
using MyCrm.Comments.GrpcContracts;
using MyCrm.Deposits.Grpc;
using MyCrm.Kyc.Grpc;
using MyCrm.MyCrmTradersUtmParametersGrpcContracts;
using MyCrm.PaymentReport.GrpcContracts;
using MyCrm.TraderMarketingSalesData.Grpc.Backoffice;
using MyCrm.TraderOnlineData.Grpc;
using MyCrm.TradersDocuments.Grpc;
using SimpleTrading.Deposit.Grpc;

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

            RegisterGrpcService(builder);
            RegisterTableStorage(builder);
            RegisterServices(builder);
        }

        private void RegisterServices(ContainerBuilder builder)
        {
            builder.RegisterType<BackofficeOfficeService>().As<IBackofficeOfficeService>().SingleInstance();

            builder.RegisterType<LastSearchUserCache>().As<ILastSearchUserCache>().SingleInstance();

            builder.RegisterType<BoUsersService>().As<IBoUsersService>().SingleInstance();
            
            builder.RegisterType<ChartService>().As<IChartService>();
        }

        private static void RegisterTableStorage(ContainerBuilder builder)
        {
            builder
                .RegisterInstance(Program.Settings.TableStorageConnectionString.CreateRolesRepository())
                .As<IBackofficeRolesRepository>()
                .SingleInstance();

            builder
                .RegisterInstance(Program.Settings.TableStorageConnectionString.CreateFiltersPresetsRepository())
                .As<IBackofficeFiltersPresetRepository>()
                .SingleInstance();
        }

        private static void RegisterGrpcService(ContainerBuilder builder)
        {

            builder.RegisterType<MyCrmAccountTransactionsGrpcServiceMoq>().As<IMyCrmAccountTransactionsGrpcService>()
                .SingleInstance();

            builder.RegisterType<MyCrmActiveDealsGrpcServiceMoq>().As<IMyCrmActiveDealsGrpcService>().SingleInstance();

            builder.RegisterType<MyCrmKycGrpcServiceMoq>().As<IMyCrmKycGrpcService>().SingleInstance();

            builder.RegisterType<MyCrmWriterTraderMarketingSalesDataForBackofficeGrpcServiceMoq>()
                .As<IMyCrmWriterTraderMarketingSalesDataForBackofficeGrpcService>().SingleInstance();

            builder.RegisterType<MyCrmReaderTraderMarketingSalesDataForBackofficeGrpcServiceMoq>()
                .As<IMyCrmReaderTraderMarketingSalesDataForBackofficeGrpcService>().SingleInstance();

            builder.RegisterType<MyCrmAuditLogGrpcServiceMoq>().As<IMyCrmAuditLogGrpcService>().SingleInstance();

            builder.RegisterType<MyCrmUsersAuthGrpcServiceMoq>().As<IMyCrmUsersAuthGrpcService>().SingleInstance();

            builder.RegisterType<MyCrmPhonePoolGrpcServiceMoq>().As<IMyCrmPhonePoolGrpcService>().SingleInstance();

            builder.RegisterType<MyCrmCallsGrpcServiceMoq>().As<IMyCrmCallsGrpcService>().SingleInstance();

            builder.RegisterType<MyCrmBusinessCategoriesGrpcServiceMoq>().As<IMyCrmBusinessCategoriesGrpcService>()
                .SingleInstance();

            builder.RegisterType<MyCrmCommentsGrpcServiceMoq>().As<IMyCrmCommentsGrpcService>().SingleInstance();

            builder.RegisterType<MyCrmAffiliateAccessServiceGrpcMoq>().As<IMyCrmAffiliateAccessServiceGrpc>().SingleInstance();

            builder.RegisterType<MyCrmTraderOnlineDataGrpcServiceMoq>().As<IMyCrmTraderOnlineDataGrpcService>()
                .SingleInstance();

            builder.RegisterType<MyCrmTradersDocumentsGrpcServiceMoq>().As<IMyCrmTradersDocumentsGrpcService>()
                .SingleInstance();

            builder.RegisterType<MyCrmAutoOwnerProfileGrpcServicesMoq>().As<IMyCrmAutoOwnerProfileGrpcServices>()
                .SingleInstance();

            builder.RegisterType<MyCrmDepositsGrpcServiceMoq>().As<IMyCrmDepositsGrpcService>().SingleInstance();

            builder.RegisterType<DepositManagerGrpcServiceMoq>().As<IDepositManagerGrpcService>().SingleInstance();

            builder.RegisterType<MyCrmTraderUrmParametersGrpcServiceMoq>().As<IMyCrmTraderUrmParametersGrpcService>()
                .SingleInstance();

            builder.RegisterType<MyCrmPaymentsReportGrpcServiceMoq>().As<IMyCrmPaymentsReportGrpcService>().SingleInstance();
        }
    }
}