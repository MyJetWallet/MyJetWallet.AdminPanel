using System.Collections.Generic;
using Service.AssetsDictionary.Domain.Models;
using Service.AssetsDictionary.MyNoSql;

namespace Backoffice.Services.Assets
{
    public class AssetItem
    {
        public Asset Asset { get; set; }
        public AssetPaymentSettingsNoSqlEntity PaymentSettings { get; set; }
        public List<string> Brands { get; set; }
    }
}