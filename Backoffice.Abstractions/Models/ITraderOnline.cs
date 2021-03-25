using System;
using Backoffice.Abstractions.Bo;
using Backoffice.ReflectionSearch;

namespace Backoffice.Abstractions.Models
{
    public interface ITraderOnline : IOwnershipHolder
    {
        string TraderId { get; }
        DateTime LastTime { get; }
        string Place { get; }
        string UserAgent { get; }
        public string Email { get; }
        public string FullName { get; }
        public string Country { get; }
    }

    public class TraderOnlineModel : ITraderOnline
    {
        public string TraderId { get; set; }
        public DateTime LastTime { get; set; }
        public string Place { get; set; }
        [Searchable] public string UserAgent { get; set; }
        [Searchable] public string Email { get; set; }
        [Searchable] public string FullName { get; set; }
        [Searchable] public string Country { get; set; }
        [Searchable] public string Owner { get; set; }
        [Searchable] public string AssignedUserId { get; set; }
        [Searchable] public string RetentionManager { get; set; }
    }
}