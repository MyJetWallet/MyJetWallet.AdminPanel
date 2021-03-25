using Backoffice.Abstractions.Bo;
using MyCrm.Auth.GrpcContracts.Models;
using MyCrm.BusinessCategories.Grpc.Models;
using System;

namespace Backoffice.Services.Backoffice
{
    public static class MapperUtils
    {
        public static IBackOfficeUser ToDomain(this BackofficeUserGrpcModel src)
        {
            return new BackOfficeUser
            {
                Id = src.Id,
                Registered = src.Registered,
                IsBlocked = src.IsBlocked,
                Ip = src.Ip,
                PersonalName = src.PersonalName,
                IsAdmin = src.IsAdmin,
                CanLoginEdit = src.CanLoginEdit,
                CanEditAffiliates = src.CanEditAffiliates,
                AllDataAccess = src.AllDataAccess,
                AllOfficeDataAccess = src.AllOfficeDataAccess,
                ExposePersonalData = src.ExposePersonalData,
                CanSeeDataWithNoOffice = src.CanSeeDataWithNoOffice,
                ReferralLink = src.ReferralLink,
                CanEditReferralLinks = src.CanEditReferralLinks,
                ChangeIsInternal = src.ChangeIsInternal,
                CanDoRealFund = src.CanDoRealFund,
                CanUseBalanceCorrection = src.CanUseBalanceCorrection,
                Roles = src.Roles ?? Array.Empty<string>(),
                Countries = src.Countries,
                DataOwnership = src.DataOwnership,
                InternalPhoneNumberId = src.InternalPhoneNumberId,
                AssignedPhonePoolId = src.AssignedPhonePoolId,
                DataOwnerships = src.DataOwnerships,
                LastLogInAttempt = src.LastLogInAttempt,
                CertAliases = src.CertAliases,
                CanDownloadData = src.CanDownloadData
            };
        }
        
        public static BackofficeUserGrpcModel ToGrpc(this IBackOfficeUser src)
        {
            return new BackofficeUserGrpcModel
            {
                Id = src.Id,
                Registered = src.Registered,
                IsBlocked = src.IsBlocked,
                Ip = src.Ip,
                PersonalName = src.PersonalName,
                IsAdmin = src.IsAdmin,
                CanLoginEdit = src.CanLoginEdit,
                CanEditAffiliates = src.CanEditAffiliates,
                AllDataAccess = src.AllDataAccess,
                AllOfficeDataAccess = src.AllOfficeDataAccess,
                ExposePersonalData = src.ExposePersonalData,
                CanSeeDataWithNoOffice = src.CanSeeDataWithNoOffice,
                ReferralLink = src.ReferralLink,
                CanEditReferralLinks = src.CanEditReferralLinks,
                Roles = src.Roles,
                Countries = src.Countries,
                DataOwnership = src.DataOwnership,
                InternalPhoneNumberId = src.InternalPhoneNumberId,
                AssignedPhonePoolId = src.AssignedPhonePoolId,
                DataOwnerships = src.DataOwnerships,
                LastLogInAttempt = src.LastLogInAttempt,
                CertAliases = src.CertAliases,
                ChangeIsInternal = src.ChangeIsInternal,
                CanDoRealFund = src.CanDoRealFund,
                CanUseBalanceCorrection = src.CanUseBalanceCorrection,
                CanDownloadData = src.CanDownloadData
            };
        }

        public static IOffice ToDomain(this BusinessCategoryGrpcModel src)
        {
            return new Office
            {
                Id = src.Id,
                Name = src.Name,
                IsDisabled = src.IsDisabled,
                PhonePoolId = src.PhonePoolId
            };
        }
        
        public static BusinessCategoryGrpcModel ToGrpc(this IOffice src)
        {
            return new BusinessCategoryGrpcModel
            {
                Id = src.Id,
                Name = src.Name,
                IsDisabled = src.IsDisabled,
                PhonePoolId = src.PhonePoolId
            };
        }
    }
}