using System;
using System.Linq;
using Backoffice.Abstractions.Models;
using Backoffice.Abstractions.PropertyMocks;
using MyCRM.Logs.GrpcContracts.Models;

namespace Backoffice.Services.Logs
{
    public static class MapperUtils
    {
        private const string AffId = "affId";
        private const string BrandId = "brandId";
        
        public static IRegisterLogModel ToDomain(this RegistrationEventGrpcModel src)
        {
            return new RegisterLogModel
            {
                Registered = src.Registered,
                Id = src.Id,
                Email = src.Email,
                FullName = src.FullName,
                RegistrationCountryCode = src.RegistrationCountryCode,
                KycCountryCode = src.KycCountryCode,
                Phone = new PhoneField(src.Phone),
                Ip = src.Ip,
                UserAgent = src.UserAgent,
                Owner = src.Owner,
                AssignedBackOfficeUserId = src.AssignedBackOfficeUserId,
                RedirectedFrom = src.RedirectedFrom,
                LandingPageId = src.LandingPageId,
                Language = src.Language,
                CountryCodeByIp = src.CountryCodeByIp,
                ExternalData = src.ExternalData?.Select(itm => RegisterLogExternalData.Create(itm.Key,itm.Value)).ToArray() ?? Array.Empty<IRegisterLogExternalData>(),
                IsInternal = src.IsInternal,
                TradingStatus = src.TradingStatus.Id,
                CrmStatus = src.CrmStatus.Id,
                RetentionManagerId = src.RetentionManagerId,
                LastContactDate = src.LastContactDate,
                Balance = src.Balance,
                NextCallDate = src.NextCallDate,
                ActivationDate = src.ActivationDate,
                AffId = src.ExternalData?.FirstOrDefault(itm => itm.Key == AffId)?.Value ?? "None", 
                BrandId = src.ExternalData?.FirstOrDefault(itm => itm.Key == BrandId)?.Value ?? "None", 
                LastComment = src.LastComment,
                UtmCampaing = src.UtmCampaing
            };
        }
    }
}