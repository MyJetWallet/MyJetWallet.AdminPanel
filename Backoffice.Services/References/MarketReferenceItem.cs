using System.Collections.Generic;
using Service.AssetsDictionary.Domain.Models;

namespace Backoffice.Services.References
{
    public class MarketReferenceItem
    {
        public MarketReference Reference { get; set; }
        
        public List<string> Brands { get; set; }
    }
}