using System;
using Backoffice.Abstractions.PropertyMocks;

namespace Backoffice.Abstractions.Models
{
    public enum BoKycState
    {
        NeedToFillData,
        WaitingForApprove,
        Approved,
        RestrictedCountry
    }
    
    public interface ITraderToCheckKyc
    {
        string Id { get; }
        
        EmailField Email { get; }
        
        string FullName { get; }
        
        string CountryIso3 { get; }
        
        DateTime Registered { get; }
        
        DateTime? Approved { get; }
        
        bool MissingDocuments { get; }
    }
    
    public class TraderToCheckKyc : ITraderToCheckKyc
    {
        public string Id { get; set; }
        public EmailField Email { get; set; }
        public string FullName { get; set; }
        public string CountryIso3 { get; set; }
        public DateTime Registered { get; set; }
        public DateTime? Approved { get; set; }
        public bool MissingDocuments { get; set; }
    }
}