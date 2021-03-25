using System;

namespace Backoffice.Abstractions.Models
{
    public interface IActiveDealModel
    {
        DateTime DateTime { get; }
        string Id { get; }
        string Instrument { get; }
        BoAccountTransactionType Operation { get; }
        double Volume { get; }
        double Pl { get; }
        double Swaps { get; }
        double Commissions { get; }
        string AccountId { get; }
        double OpenPrice { get; }
        TimeSpan OpenDuration { get; }
    }
    
    public class ActiveDealModel : IActiveDealModel
    {
        public DateTime DateTime { get; set;}
        public string Id { get; set;}
        public string Instrument { get; set;}
        public BoAccountTransactionType Operation { get; set;}
        public double Volume { get; set;}
        public double Pl { get; set;}
        public double Swaps { get; set;}
        public double Commissions { get; set;}
        public string AccountId { get; set;}
        public double OpenPrice { get; set;}
        public TimeSpan OpenDuration { get; set; }
    }
}