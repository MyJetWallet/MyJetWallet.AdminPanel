using Service.ClientWallets.Grpc.Models;

namespace Backoffice.Components.Clients.Model
{
    public class ClientResultModel
    {
        public ClientResultModel(SearchWallet wallet, bool emailConfirmed)
        {
            BrokerId = wallet.BrokerId;
            BrandId = wallet.BrandId;
            ClientId = wallet.ClientId;
            Count = wallet.Count;
            EmailConfirmed = emailConfirmed;
        }
        public string BrokerId { get; set; }

        public string BrandId { get; set; }

        public string ClientId { get; set; }

        public int Count { get; set; }
        
        public bool EmailConfirmed { get; set; }
        
        //public bool PhoneConfirmed { get; set; }
    }
}