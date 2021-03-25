using Allegiance.Blazor.Highcharts.Services;
using Autofac;
using Backoffice.Abstractions.Bo;
using Backoffice.Abstractions.Services;
using Backoffice.Mocks;
using Backoffice.RestServices;
using Backoffice.Services;
using Backoffice.Services.Accounts;
using Backoffice.Services.ActiveDeals;
using Backoffice.Services.Affiliates;
using Backoffice.Services.AuditLog;
using Backoffice.Services.Backoffice;
using Backoffice.Services.Comments;
using Backoffice.Services.Deposits;
using Backoffice.Services.KYC;
using Backoffice.Services.Logs;
using Backoffice.Services.Online;
using Backoffice.Services.PersonalData;
using Backoffice.Services.PhonePool;
using Backoffice.Services.Status;
using Backoffice.Services.Transactions;
using Backoffice.Services.Withdrawal;
using Backoffice.TableStorage;
using MyCrm.Accounts.Grpc;
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
using MyCRM.Logs.GrpcContracts;
using MyCrm.MyCrmTradersUtmParametersGrpcContracts;
using MyCrm.PaymentReport.GrpcContracts;
using MyCrm.PersonalData.Grpc;
using MyCrm.TraderCrmStatuses.Grpc;
using MyCrm.TraderMarketingSalesData.Grpc.Backoffice;
using MyCrm.TraderOnlineData.Grpc;
using MyCrm.TradersDocuments.Grpc;
using MyCrm.Withdrawals.Grpc;
using Service.AssetsDictionary.Client;
using SimpleTrading.Deposit.Grpc;

namespace Backoffice.Modules
{
    public class ServiceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            

            RegisterGrpcService(builder);
            RegisterTableStorage(builder);
            RegisterServices(builder);
        }

        private void RegisterServices(ContainerBuilder builder)
        {
            builder.RegisterType<PersonalDataService>().As<IPersonalDataService>().SingleInstance();

            builder.RegisterType<AccountsService>().As<IAccountService>().SingleInstance();
            builder.RegisterType<ActiveDealsService>().As<IActiveDealService>().SingleInstance();
            builder.RegisterType<LogsService>().As<ILogService>().SingleInstance();
            builder.RegisterType<TradersKycService>().As<ITradersKycService>().SingleInstance();
            builder.RegisterType<StatusService>().As<IStatusService>().SingleInstance();
            builder.RegisterType<TransactionService>().As<ITransactionsService>().SingleInstance();
            builder.RegisterType<AuditLogService>().As<IAuditLogService>().SingleInstance();
            
            builder.RegisterType<PhonePoolService>().As<IPhonePoolService>().SingleInstance();
            builder.RegisterType<BackofficeOfficeService>().As<IBackofficeOfficeService>().SingleInstance();

            builder.RegisterType<CommentsService>().As<ICommentsService>().SingleInstance(); 

            builder.RegisterType<AffiliatesService>().As<IAffiliateService>().SingleInstance();

            builder.RegisterType<TraderOnlineService>().As<ITradersOnlineService>().SingleInstance();

            builder.RegisterType<DepositService>().As<IDepositService>().SingleInstance();

            builder.RegisterType<WithdrawalService>().As<IWithdrawalService>().SingleInstance();

            builder.RegisterType<PaymentReportService>().As<IPaymentServiceReport>().SingleInstance();

            builder.RegisterType<LastSearchUserCache>().As<ILastSearchUserCache>().SingleInstance();

            builder.RegisterType<BoUsersService>().As<IBoUsersService>().SingleInstance();

            builder.RegisterInstance(new DepositRestClient(Program.Settings.MonfexApi, Program.Settings.HpApi, Program.Settings.AlianceApi)).AsSelf().SingleInstance();

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
            builder.RegisterType<MyCrmPersonalDataGrpcServiceMoq>().As<IMyCrmPersonalDataGrpcService>().SingleInstance();

            builder.RegisterType<MyCrmAccountsGrpcServiceMoq>().As<IMyCrmAccountsGrpcService>().SingleInstance();

            builder.RegisterType<MyCrmAccountTransactionsGrpcServiceMoq>().As<IMyCrmAccountTransactionsGrpcService>()
                .SingleInstance();

            builder.RegisterType<MyCrmActiveDealsGrpcServiceMoq>().As<IMyCrmActiveDealsGrpcService>().SingleInstance();

            builder.RegisterType<MyCrmLogsGrpcServiceMoq>().As<IMyCrmLogsGrpcService>().SingleInstance();

            builder.RegisterType<MyCrmKycGrpcServiceMoq>().As<IMyCrmKycGrpcService>().SingleInstance();

            builder.RegisterType<MyCrmTraderCrmStatusesGrpcServiceMoq>().As<IMyCrmTraderCrmStatusesGrpcService>()
                .SingleInstance();

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

            builder.RegisterType<MyCrmWithdrawalsGrpcServiceMoq>().As<IMyCrmWithdrawalsGrpcService>().SingleInstance();

            builder.RegisterType<MyCrmTraderUrmParametersGrpcServiceMoq>().As<IMyCrmTraderUrmParametersGrpcService>()
                .SingleInstance();

            builder.RegisterType<MyCrmPaymentsReportGrpcServiceMoq>().As<IMyCrmPaymentsReportGrpcService>().SingleInstance();
        }
    }
}