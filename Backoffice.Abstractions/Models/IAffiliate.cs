namespace Backoffice.Abstractions.Models
{
    public interface IAffiliate
    {
        string ApiKey { get; }

        string Ips { get; }

        string AffId { get; }

        double DepFrom { get; }

        string Description { get; }
    }

    public class Affiliate : IAffiliate
    {
        public string ApiKey { get; set; }

        public string Ips { get; set; }

        public string AffId { get; set; }

        public double DepFrom { get; set; }

        public string Description { get; set; }
    }
}