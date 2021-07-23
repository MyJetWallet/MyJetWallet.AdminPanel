using System.Collections.Generic;

namespace Backoffice
{
    public static class Menu
    {
        public const string Assets = "Assets";
        public const string SpotInstruments = "SpotInstruments";
        public const string Liquidity = "Liquidity";
        public const string Prices = "Prices";
        public const string Simulations = "Simulations";
        public const string Clients = "Clients";
        public const string BitGo = "BitGo";
        public const string SmsSender = "SmsSender";
        public const string Converter = "Converter";
        public const string ExternalMarkets = "ExternalMarkets";
        public const string Fees = "Fees";
        public const string Notifications = "Notifications";
        public const string Monitor = "Monitor";
        public const string SocialNetworkAuth = "SocialNetworkAuth";
        public const string References = "References";
        public const string CashOperations = "CashOperations";
        public const string MarketInfo = "MarketInfo";
        public const string Templates = "Templates";
    }
    
    public static class Actions
    {
        public const string DeleteRecordsRight = "DeleteRecords";
        public const string EditAchievementStatus = nameof(EditAchievementStatus);
    }

    public static class BackOfficeRightCache
    {
        private static readonly List<BackOfficeRight> Rights = new()
        {
            BackOfficeRight.Create(Menu.Assets, "Access to the assets menu"),
        };

        public static List<BackOfficeRight> Get() => Rights;
    }

    public class BackOfficeRight
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public static BackOfficeRight Create(string id, string name)
        {
            return new()
            {
                Id = id,
                Name = name
            };
        }
    }

    public static class MenuGenerator
    {
        //oi pack
        private static readonly List<NavsItem> MenuItems = new()
        {
            NavsItem.Create("Assets", "Assets", "list-rich", Menu.Assets),
            NavsItem.Create("Spot Instruments", "SpotInstruments", "list-rich", Menu.SpotInstruments),
            NavsItem.Create("Market References", "References","list-rich",Menu.References),
            NavsItem.Create("Liquidity", "LiquidityEngine", "list-rich", Menu.Liquidity),
            NavsItem.Create("Prices", "Prices", "list-rich", Menu.Prices),
            NavsItem.Create("Simulations", "Simulations", "list-rich", Menu.Simulations),
            NavsItem.Create("Clients", "Clients", "list-rich", Menu.Clients),
            NavsItem.Create("BitGo", "BitGo", "list-rich", Menu.BitGo),
            NavsItem.Create("External Markets", "ExternalMarkets", "list-rich", Menu.ExternalMarkets),
            NavsItem.Create("SmsSender", "SmsSender", "list-rich", Menu.SmsSender),
            NavsItem.Create("Converter", "Converter", "list-rich", Menu.Converter),
            NavsItem.Create("Fees", "Fees", "list-rich", Menu.Fees),
            NavsItem.Create("Notifications", "Notifications","list-rich",Menu.Notifications),
            NavsItem.Create("Monitor", "Monitor","list-rich",Menu.Monitor),
            NavsItem.Create("Social Network Auth", "AuthTest","list-rich",Menu.SocialNetworkAuth),
            NavsItem.Create("Cash Operations", "CashOperations","list-rich",Menu.CashOperations),
            NavsItem.Create("Markets Info", "MarketInfo","list-rich",Menu.MarketInfo),
            NavsItem.Create("Templates", "Templates","list-rich",Menu.Templates),

        };
        
        public static IEnumerable<NavsItem> GenerateMenuItems() => MenuItems;
    }

    public class NavsItem
    {
        public string Name { get; set; }

        public string Href { get; set; }

        public string Icon { get; set; }

        public string Right { get; set; }

        public static NavsItem Create(string name, string href, string icon, string right)
        {
            return new()
            {
                Name = name,
                Href = href,
                Icon = icon,
                Right = right
            };
        }
    }
}