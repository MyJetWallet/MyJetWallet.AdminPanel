using System;
using System.Linq;
using Backoffice.Abstractions.Bo;

namespace Backoffice.Abstractions.PropertyMocks
{
    public struct EmailField
    {
        private readonly string _email;

        public EmailField(string email)
        {
            if (email == null)
                email = "";

            _email = email.ToLower();
        }

        private static string GetAnonimizedEmail(string email)
        {

            const string defaultEmail = "***";

            try
            {
                var emailParts = email.Split('@');

                if (emailParts.Length == 1)
                    return defaultEmail;

                if (emailParts[0].Length == 0)
                    return defaultEmail;

                return emailParts[0][0] + "***" + emailParts[0].Last() + "@" + emailParts[1];
            }
            catch (Exception)
            {
                return defaultEmail;
            }
        }

        public string GetValue(IBackOfficeUser user)
        {
            if (string.IsNullOrEmpty(_email))
                return _email;

            if (user == null)
                return GetAnonimizedEmail(_email ?? string.Empty);

            if (user.IsAdmin || user.ExposePersonalData)
                return _email;

            return GetAnonimizedEmail(_email ?? string.Empty);
        }

        public string GetRawValue()
        {
            return _email;
        }
        public override string ToString()
        {
            return GetAnonimizedEmail(_email ?? string.Empty);
        }
    }
}