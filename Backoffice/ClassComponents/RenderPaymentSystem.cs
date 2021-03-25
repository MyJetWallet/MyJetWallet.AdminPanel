using System.Collections.Generic;
using Microsoft.AspNetCore.Components;

namespace Backoffice.ClassComponents
{
    public class PaymentSystemComponent : ComponentBase
    {
        private const string CoinPayments = "CoinPayment";
        private const string Neteller = "Neteller";
        private const string Octapay = "pciDssOctapayBankCards";
        private const string RoyalPay = "pciDssRoyalPayBankCards";
        
        private const string PciDssTexcentBankCards = "pciDssTexcentBankCards";
        private const string Texcent = "Texcent";
        private const string TextcentBankCards = "Texcent";
        
        private const string WireTransfer = "WireTransfer";

        private const string BaseImageUrl = "/images/payment-systems";

        [Parameter]
        public string PaymentSystem { get; set; }

        protected (string image, string name) GetPaymentSystemData()
        {
            if (PaymentSystem == CoinPayments)
            {
                var image = $"{BaseImageUrl}/coinpayment.png";
                return (image, "CoinPayments - Crypto");
            }

            if (PaymentSystem == Neteller)
            {
                var image = $"{BaseImageUrl}/neteller.png";
                return (image, "Neteller - Internal");
            }
            
            if (PaymentSystem == Octapay)
            {
                var image = $"{BaseImageUrl}/octapay.png";
                return (image, "Octapay - Bank cards");
            }
            
            if (PaymentSystem == PciDssTexcentBankCards || PaymentSystem == Texcent || PaymentSystem == TextcentBankCards)
            {
                var image = $"{BaseImageUrl}/texcent.png";
                return (image, "Texcent - Bank cards");
            }      
            
            if (PaymentSystem == RoyalPay)
            {
                var image = $"{BaseImageUrl}/royalpay.png";
                return (image, "RoyalPay - Bank cards");
            }
            
            if (PaymentSystem == WireTransfer)
            {
                var image = $"{BaseImageUrl}/wire-transfer.jpg";
                return (image, "Wire Transfer");
            }

            return (null, PaymentSystem);
        }

        public static IEnumerable<string> GetPaymentSystems()
        {
            return new List<string>
            {
                CoinPayments,
                Neteller,
                Octapay,
                RoyalPay,
                WireTransfer,
                PciDssTexcentBankCards
            };
        }
    }
}