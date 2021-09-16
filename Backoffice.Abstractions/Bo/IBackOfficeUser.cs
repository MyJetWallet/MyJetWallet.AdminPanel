using System;
using System.Collections.Generic;
using Backoffice.Abstractions.Access;

namespace Backoffice.Abstractions.Bo
{
    public interface IBackOfficeUser
    {
        string Id { get; }
        DateTime Registered { get; }
        bool IsBlocked { get; }
        string PersonalName { get; }
        bool IsAdmin { get; }
        public string PhoneNumber { get;  }
        public string TelegramNickName { get;  }
        IEnumerable<string> Roles { get; }
        IEnumerable<string> CertAliases { get; }
        
        IReadOnlyList<BoRight> Rights { get; }
    }
    
    public class BackOfficeUser : IBackOfficeUser
    {
        public string Id { get; set;}
        
        public DateTime Registered { get; set;}
        
        public bool IsBlocked { get; set;}
        
        public string PersonalName { get; set;}
        
        public bool IsAdmin { get; set;}
        
        public string PhoneNumber { get; set; }
        
        public string TelegramNickName { get; set; }
        
        public List<string> Roles { get; set; } = new();
        
        public List<string> CertAliases { get; set; } = new();

        public IReadOnlyList<BoRight> Rights { get; set; } = new List<BoRight>();

        IEnumerable<string> IBackOfficeUser.Roles => Roles;
        IEnumerable<string> IBackOfficeUser.CertAliases => CertAliases;
    }
}