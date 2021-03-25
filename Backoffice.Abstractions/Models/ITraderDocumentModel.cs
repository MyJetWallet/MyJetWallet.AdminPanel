using System;

namespace Backoffice.Abstractions.Models
{
    public enum TraderDocumentType
    {
        Id,
        ProofOfAddress,
        FrontCard,
        BackCard,
        DepositLetter,
        Other
    }

    public interface ITraderDocumentModel
    {
        string TraderId { get; }
        string Id { get; }
        TraderDocumentType DocumentType { get; }
        DateTime DateTime { get; }
        string Mime { get; }
        bool VisibleForClient { get; set; }
        string Uploader { get; }
    }

    public class TraderDocumentModel : ITraderDocumentModel
    {
        public string TraderId { get; set; }
        public string Id { get; set; }
        public TraderDocumentType DocumentType { get; set; }
        public DateTime DateTime { get; set; }
        public string Mime { get; set; }
        public bool VisibleForClient { get; set; }
        public string Uploader { get; set; }
    }
}