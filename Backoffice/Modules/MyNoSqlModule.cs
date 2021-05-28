using Autofac;
using MyJetWallet.BitGo.Settings.NoSql;
using MyNoSqlServer.Abstractions;
using Service.AssetsDictionary.MyNoSql;

namespace Backoffice.Modules
{
    public class MyNoSqlModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            RegisterMyNoSqlWriter<BitgoAssetMapEntity>(builder, BitgoAssetMapEntity.TableName);
            RegisterMyNoSqlWriter<BitgoCoinEntity>(builder, BitgoAssetMapEntity.TableName);
            RegisterMyNoSqlWriter<AssetPaymentSettingsNoSqlEntity>(builder, AssetPaymentSettingsNoSqlEntity.TableName);
        }

        private static void RegisterMyNoSqlWriter<TEntity>(ContainerBuilder builder, string table)
            where TEntity : IMyNoSqlDbEntity, new()
        {
            builder.Register(ctx => new MyNoSqlServer.DataWriter.MyNoSqlServerDataWriter<TEntity>(
                    Program.ReloadedSettings(e => e.MyNoSqlWriterUrl), table, true))
                .As<IMyNoSqlServerDataWriter<TEntity>>()
                .SingleInstance();
        }
    }
}