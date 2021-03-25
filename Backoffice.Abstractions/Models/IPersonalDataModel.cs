using System;
using System.Collections.Generic;
using Backoffice.Abstractions.PropertyMocks;

namespace Backoffice.Abstractions.Models
{
    public interface IPersonalDataExternalData
    {
        string Key { get; set; }
        string Value { get; set; }
    }

    public class PersonalDataExternalData : IPersonalDataExternalData
    {
        public string Key { get; set; }
        public string Value { get; set; }

        public static PersonalDataExternalData Create(string key, string value)
        {
            return new PersonalDataExternalData
            {
                Key = key,
                Value = value
            };
        }
    }

    public interface IAdditionalPhoneModel
    {
        string Id { get; }
        string TraderId { get; }
        string Phone { get; }
    }

    public class AdditionalPhoneModel : IAdditionalPhoneModel
    {
        public string Id { get; set; }
        public string TraderId { get; set; }
        public string Phone { get; set; }

        public static AdditionalPhoneModel Create(string id, string traderId, string phone)
        {
            return new AdditionalPhoneModel
            {
                Id = id,
                TraderId = traderId,
                Phone = phone
            };
        }
    }

    public interface IBoAdditionalPhoneModel
    {
        string Id { get; }
        string Phone { get; }
    }

    public class BoAdditionalPhoneModel : IBoAdditionalPhoneModel
    {
        public string Id { get; set; }
        public string Phone { get; set; }

        public static BoAdditionalPhoneModel Create(string id, string phone)
        {
            return new BoAdditionalPhoneModel
            {
                Id = id,
                Phone = phone
            };
        }
    }

    public interface IPersonalDataUpdateModel
    {
        string Id { get; }
        string Address { get; }
        string City { get; }
        string Phone { get; }
        string CountryOfCitizenship { get; }
        string CountryOfResidence { get; }
        string FirstName { get; }
        string LastName { get; }
        string ZipCode { get; }
        DateTime? BirthDay { get; }
        bool MissDocuments { get; }
    }

    public class PersonalDataUpdateModel : IPersonalDataUpdateModel
    {
        public string Id { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Phone { get; set; }
        public string CountryOfCitizenship { get; set; }
        public string CountryOfResidence { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ZipCode { get; set; }
        public DateTime? BirthDay { get; set; }
        public bool MissDocuments { get; set; }

        public static PersonalDataUpdateModel Create(string id)
        {
            return new PersonalDataUpdateModel
            {
                Id = id
            };
        }
    }

    public interface IPersonalDataModel
    {
        string Id { get; set;}
        DateTime Registered { get; set;}
        string Email { get; set;}
        string Address { get; set;}
        string Phone { get; set;}
        string City { get; set;}
        string ZipCode { get; set;}
        string FirstName { get; set;}
        string LastName { get; set;}
        string CountryOfRegistration { get; set;}
        DateTime? BirthDay { get; set; }
        bool EmailIsConfirmed { get; set;}
        bool IsInternal { get; set;}
        string OwnerId { get; set;}
        string AssignedUserId { get; set;}
        string CrmStatus { get; set;}
        string CountryOfCitizenship { get; set;}
        string CountryOfResidence { get; set;}
        IEnumerable<IPersonalDataExternalData> ExternalInfo { get; set;}
        string TradingStatus { get; set;}
        string RetentionManagerId { get; set;}
        DateTime NextCallDate { get; set;}
        DateTime ActivationDate { get; set;}
        bool MissDocuments { get; set;}
    }

    public class PersonalDataModel : IPersonalDataModel
    {
        public string Id { get; set; }
        public DateTime Registered { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string SkypeId { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CountryOfRegistration { get; set; }
        public DateTime? BirthDay { get; set; }
        public bool EmailIsConfirmed { get; set; }
        public bool IsInternal { get; set; }
        public string OwnerId { get; set; }
        public string AssignedUserId { get; set; }
        public string CrmStatus { get; set; }
        public string CountryOfCitizenship { get; set; }
        public string CountryOfResidence { get; set; }
        public IEnumerable<IPersonalDataExternalData> ExternalInfo { get; set; }
        public string TradingStatus { get; set; }
        public string RetentionManagerId { get; set; }
        public DateTime NextCallDate { get; set; }
        public DateTime ActivationDate { get; set; }
        public bool MissDocuments { get; set; }
    }

    public interface ITraderUtm
    {
        string Id { get; }
        string TraderId { get; }
        string Key { get; }
        string Value { get; }
    }
    
    public class TraderUtm : ITraderUtm
    {
        public string Id { get; set;}
        public string TraderId { get; set;}
        public string Key { get; set;}
        public string Value { get; set;}
    }
}