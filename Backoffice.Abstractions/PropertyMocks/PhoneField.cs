using System;
using Backoffice.Abstractions.Bo;

namespace Backoffice.Abstractions.PropertyMocks
{
    public class PhoneField
    {
        private readonly string _phone;

        public PhoneField(string phone)
        {
            if (phone == null)
                phone = "";

            _phone = phone.ToLower();
        }
        
        private static string GetAnonimizedPhone(string phone)
        {

            const string defaultPhone = "******";

            try
            {
                return phone.Length < 4 ? defaultPhone : $"{phone.Substring(0,4)}*********";
            }
            catch (Exception)
            {
                return defaultPhone;
            }
        }
        
        public string GetValue(IBackOfficeUser user)
        {
            if (string.IsNullOrEmpty(_phone))
                return _phone;

            if (user == null)
                return GetAnonimizedPhone(_phone ?? string.Empty);

            if (user.IsAdmin || user.ExposePersonalData)
                return _phone;

            return GetAnonimizedPhone(_phone ?? string.Empty);
        }
        
        public string GetRawValue()
        {
            return _phone;
        }
        
        public override string ToString()
        {
            return GetAnonimizedPhone(_phone);
        }
    }
}