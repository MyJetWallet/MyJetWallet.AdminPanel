using System;

namespace Backoffice.Abstractions.Models
{
    public interface IAuditLogModel
    {
        string TraderId { get; }
        string Action { get; }
        string ActionId { get; }
        DateTime DateTime { get; }
        string Message { get; }
        string TechData { get; }
        string Author { get; }
    }

    public class AuditLogModel : IAuditLogModel
    {
        public string TraderId { get; set; }
        public string Action { get; set; }
        public string ActionId { get; set; }
        public DateTime DateTime { get; set; }
        public string Message { get; set; }
        public string TechData { get; set; }
        public string Author { get; set; }
    }
}