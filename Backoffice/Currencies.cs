using System.Collections.Generic;
using System.Linq;

namespace Backoffice
{
    public class Currency
    {
        public Currency(string id, string symbol, string cultureName,
            string engNameSingle, string engNamePlural,
            string code)
        {
            EngNamePlural = engNamePlural;
            EngNameSingle = engNameSingle;
            CultureName = cultureName;
            Symbol = symbol;
            Id = id;
            IsoCode = code;
        }

        public string Id { get; private set; }
        public string Symbol { get; private set; }
        public string CultureName { get; private set; } 
        public string EngNameSingle { get; private set; }
        public string EngNamePlural { get; private set; }
        
        public string IsoCode { get; private set; }

        public override string ToString() => Symbol;
    }

    public static class Currencies
    {
        public const string Usd = "USD";
        public const string Cnh = "CNH";
        public const string Rub = "RUB";

        private static readonly Dictionary<string, Currency> Crns = new ()
        {
            {Usd, new Currency(Usd, "$", "en-US", "dollar", "dollars", "840")},
            {Cnh, new Currency(Cnh, "元", "zh-CN", "renminbi", "renminbis", "156")},
            {Rub, new Currency(Rub, "₽", "ru-RU", "ruble", "rubles",  "643")}
        };

        public static Currency Currency(string id)
        {
            if (string.IsNullOrEmpty(id))
                id = Usd;

            return Crns.ContainsKey(id) 
                ? Crns[id] 
                : new Currency(id, id, "en-US", id, id, "0");
        }

        public static IEnumerable<Currency> GetAll()
        {
            return Crns.Values;
        }

        public static string GetCurrenctName(this string iso)
        {
            if (iso == "978")
            {
                return "Euro";
            }

            foreach (var crnsValue in Crns.Values.Where(crnsValue => crnsValue.IsoCode == iso))
            {
                return crnsValue.EngNameSingle;
            }

            return iso;
        }
    }
}
