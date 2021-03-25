using System;
using Backoffice.Abstractions.PropertyMocks;
using Backoffice.ReflectionSearch;

namespace Backoffice.Abstractions.Models
{
    public interface IRegisterLogExternalData
    {
        string Key { get; set; }
        string Value { get; set; }
    }

    public interface IRegisterLogCrmStatus
    {
        string Id { get; set; }
        string Value { get; set; }
    }

    public interface IRegisterLogModel
    {
        DateTime Registered { get; set; }
        string Id { get; set; }
        string Email { get; set; }
        string BrandId { get; set; }
        // EmailField Email { get; set; }
        string FullName { get; set; }
        // StringProperty FullName { get; set; }
        string RegistrationCountryCode { get; set; }
        string KycCountryCode { get; set; }
        PhoneField Phone { get; set; }
        string Ip { get; set; }
        string UserAgent { get; set; }
        string Owner { get; set; }
        string AssignedBackOfficeUserId { get; set; }
        string RedirectedFrom { get; set; }
        string LandingPageId { get; set; }
        string Language { get; set; }
        string CountryCodeByIp { get; set; }
        bool IsInternal { get; set; }
        string RetentionManagerId { get; set; }
        double Balance { get; set; }
        string AffId { get; set; }
        string TradingStatus { get; set; }
        string CrmStatus { get; set; }
        IRegisterLogExternalData[] ExternalData { get; set; }
        DateTime LastContactDate { get; set; }
        DateTime NextCallDate { get; set; }
        DateTime ActivationDate { get; set; }
        string UtmCampaing { get; set; }
        string LastComment { get; set; }
    }

    public class RegisterLogExternalData : IRegisterLogExternalData
    {
        public string Key { get; set; }
        public string Value { get; set; }

        public static IRegisterLogExternalData Create(string key, string value)
        {
            return new RegisterLogExternalData
            {
                Key = key,
                Value = value
            };
        }
    }

    public class RegisterLogCrmStatus : IRegisterLogCrmStatus
    {
        public string Id { get; set; }
        public string Value { get; set; }

        public static IRegisterLogCrmStatus Create(string id, string value)
        {
            return new RegisterLogCrmStatus
            {
                Id = id,
                Value = value
            };
        }
    }

    public class RegisterLogModel : IRegisterLogModel
    {
        public DateTime Registered { get; set; }
        public string Id { get; set; }
        [Searchable] public string Email { get; set; }
        [Searchable] public string BrandId { get; set; }
        [Searchable] public string FullName { get; set; }
        [Searchable] public string RegistrationCountryCode { get; set; }
        [Searchable] public string KycCountryCode { get; set; }
        public PhoneField Phone { get; set; }
        public string Ip { get; set; }
        public string UserAgent { get; set; }
        public string Owner { get; set; }
        [Searchable] public string AssignedBackOfficeUserId { get; set; }
        public string RedirectedFrom { get; set; }
        public string LandingPageId { get; set; }
        public string Language { get; set; }
        [Searchable] public string CountryCodeByIp { get; set; }
        public IRegisterLogExternalData[] ExternalData { get; set; }
        public bool IsInternal { get; set; }
        [Searchable] public string TradingStatus { get; set; }
        [Searchable] public string CrmStatus { get; set; }
        [Searchable] public string RetentionManagerId { get; set; }
        public DateTime LastContactDate { get; set; }
        public double Balance { get; set; }
        public DateTime NextCallDate { get; set; }
        [Searchable] public DateTime ActivationDate { get; set; }
        [Searchable] public string UtmCampaing { get; set; }
        [Searchable] public string AffId { get; set; }
        public string LastComment { get; set; }
    }

    public interface IAuthLogModel
    {
        DateTime Authenticated { get; }
        string Ip { get; }
        string UserAgent { get; }
        string Language { get; }
        string CountryByIp { get; }
    }

    public class AuthLogModel : IAuthLogModel
    {
        public DateTime Authenticated { get; set;}
        public string Ip { get; set;}
        public string UserAgent { get; set;}
        public string Language { get; set;}
        public string CountryByIp { get; set;}
    }
}