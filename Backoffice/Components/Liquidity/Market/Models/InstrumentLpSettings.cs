using Service.Liquidity.Engine.Domain.Models.Settings;
using Service.Liquidity.Engine.Domain.Models.Wallets;

namespace Backoffice.Components.Liquidity.Market.Models
{
    public class InstrumentLpSettings
    {
        public string Symbol { get; set; }
        public string WalletName { get; set; }
        public string WalletId { get; set; }

        public MirroringLiquiditySettings Liquidity { get; set; }
        public HedgeInstrumentSettings Hedge { get; set; }
        
        public LpWallet Wallet { get; set; }
    }
}