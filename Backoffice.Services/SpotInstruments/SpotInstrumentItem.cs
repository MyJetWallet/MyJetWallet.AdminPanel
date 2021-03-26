using System.Collections.Generic;
using Service.AssetsDictionary.Domain.Models;

namespace Backoffice.Services.SpotInstruments
{
    public class SpotInstrumentItem
    {
        public SpotInstrument Instrument { get; set; }
        
        public List<string> Brands { get; set; }
    }
}