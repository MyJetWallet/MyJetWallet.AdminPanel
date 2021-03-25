using System;
using System.Runtime.Serialization;
using MyTokenGeneratorUtils;

namespace Backoffice.Tokens
{
    
    [DataContract]
    public class ShowDocumentToken : ITokenBase
    {
        [DataMember(Order = 1)]
        public string UserId { get; set; }
        [DataMember(Order = 2)]
        public string TraderId { get; set; }
        [DataMember(Order = 3)]
        public string DocumentId { get; set; }
        [DataMember(Order = 4)]
        public DateTime Expires { get; set; }
    }
}