using System;
using System.Collections.Generic;

namespace Backoffice.Abstractions.Bo
{
    public interface IBackOfficeUser
    {
        string Id { get; set;}
        DateTime Registered { get; set;}
        bool IsBlocked { get; set;}
        string Ip { get; set;}
        string PersonalName { get; set;}
        bool IsAdmin { get; set;}
        bool CanLoginEdit { get; set;}
        bool CanEditAffiliates { get; set;}
        bool AllDataAccess { get; set;}
        bool AllOfficeDataAccess { get; set;}
        bool ExposePersonalData { get; set;}
        bool CanSeeDataWithNoOffice { get; set;}
        string ReferralLink { get; set;}
        bool CanEditReferralLinks { get; set;}
        bool ChangeIsInternal { get; set;}
        IEnumerable<string> Roles { get; set;}
        IEnumerable<string> Countries { get; set;}
        IEnumerable<string> CertAliases { get; set;}
        string DataOwnership { get; set;}
        string InternalPhoneNumberId { get; set;}
        string AssignedPhonePoolId { get; set;}
        string[] DataOwnerships { get; set;}
        DateTime? LastLogInAttempt { get; set;}
        bool CanDoRealFund { get; set; }
        bool CanUseBalanceCorrection { get; set; }
        bool CanDownloadData { get; set; }
    }
    
    public class BackOfficeUser : IBackOfficeUser
    {
        public string Id { get; set;}
        public DateTime Registered { get; set;}
        public bool IsBlocked { get; set;}
        public string Ip { get; set;}
        public string PersonalName { get; set;}
        public bool IsAdmin { get; set;}
        public bool CanLoginEdit { get; set;}
        public bool CanEditAffiliates { get; set;}
        public bool AllDataAccess { get; set;}
        public bool AllOfficeDataAccess { get; set;}
        public bool ExposePersonalData { get; set;}
        public bool CanSeeDataWithNoOffice { get; set;}
        public string ReferralLink { get; set;}
        public bool CanEditReferralLinks { get; set;}
        public bool ChangeIsInternal { get; set; }
        public IEnumerable<string> Roles { get; set;}
        public IEnumerable<string> Countries { get; set;}
        public IEnumerable<string> CertAliases { get; set; } = new List<string>();
        public string DataOwnership { get; set;}
        public string InternalPhoneNumberId { get; set;}
        public string AssignedPhonePoolId { get; set;}
        public string[] DataOwnerships { get; set;}
        public DateTime? LastLogInAttempt { get; set;}
        public bool CanDoRealFund { get; set; }
        public bool CanUseBalanceCorrection { get; set; }
        public bool CanDownloadData { get; set; }
    }
}