using System.Collections.Generic;

namespace Backoffice.Abstractions.Models
{
    public interface IAutoOwnerProfileModel
    {
        string Id { get; set;}
        string OfficeId { get; set;}
        string BrandId { get; set;}
        bool IsAffiliateTraffic { get; set;}
        IEnumerable<string> SupportedCountries { get; set;}
    }
    
    public interface IBrand
    {
        string Id { get; set;}
        string BaseDomain { get; set;}
    }
    
    public class Brand : IBrand
    {
        public string Id { get; set; }
        public string BaseDomain { get; set; }
    }

    public class AutoOwnerProfileModel : IAutoOwnerProfileModel
    {
        public string Id { get; set;}
        public string OfficeId { get; set;}
        public string BrandId { get; set;}
        public bool IsAffiliateTraffic { get; set;}
        public IEnumerable<string> SupportedCountries { get; set;}
    }
}