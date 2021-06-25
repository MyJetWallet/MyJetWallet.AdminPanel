using System.Collections.Generic;

namespace Backoffice.Components.Clients.Model
{
    public class ClientModel
    {
        public ClientModel(string brokerId, string clientId, string brandId, bool emailConfirmed)
        {
            BrokerId = brokerId;
            ClientId = clientId;
            BrandId = brandId;
            EmailConfirmed = emailConfirmed;
            Wallets = new List<WalletModel>();
        }

        public string BrokerId { get; set; }
        public string ClientId { get; set; }
        public string BrandId { get; set; }
        public bool EmailConfirmed { get; set; }
        
        public List<WalletModel> Wallets { get; set; }
    }

    public class WalletModel
    {
        public WalletModel(string name, string brokerId, string clientId, string brandId, string walletId, bool isDefault)
        {
            Name = name;
            BrokerId = brokerId;
            ClientId = clientId;
            BrandId = brandId;
            WalletId = walletId;
            IsDefault = isDefault;
        }

        public string Name { get; set; }
        
        public string BrokerId { get; set; }
        public string ClientId { get; set; }
        public string BrandId { get; set; }
        public string WalletId { get; set; }
        
        public bool IsDefault { get; set; }
    }
}