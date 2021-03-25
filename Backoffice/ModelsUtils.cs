using System;
using System.Text.Json;
using Backoffice.Abstractions.Bo;
using Backoffice.Abstractions.Models;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Backoffice
{
    public static class ModelsUtils
    {
        public const string St = "ST";
        public const string Mt = "MT";
        
        public const string LiveMt = "mtl";
        public const string DemoMt = "mtd";
        public const string LiveSt = "stl";
        public const string DemoSt = "std";
        
        public static bool IsSt(this IAccountModel account)
        {
            return account.AccountType == St;
        }
        
        public static bool IsMt(this IAccountModel account)
        {
            return account.AccountType == Mt;
        }
        
        public static bool IsSt(this string accountType)
        {
            return accountType == St;
        }
        
        public static bool IsMt(this string accountType)
        {
            return accountType == Mt;
        }

        public static bool IsLive(this string accountId)
        {
            return accountId.Contains(LiveMt) || accountId.Contains(LiveSt);
        }

        public static bool IsMonfex(this string brandName)
        {
            return brandName.ToLower().Contains("monfex");
        }
        
        public static bool IsHp(this string brandName)
        {
            return brandName.ToLower().Contains("handlepro");
        }
        
        public static string PrettyJson(this object unPrettyJson)
        {
            var options = new JsonSerializerOptions{
                WriteIndented = true
            };

            var jsonElement = JsonSerializer.Deserialize<JsonElement>(JsonConvert.SerializeObject(unPrettyJson));

            return JsonSerializer.Serialize(jsonElement, options);
        }
    }
}