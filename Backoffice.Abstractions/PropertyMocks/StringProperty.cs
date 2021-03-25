using System;
using System.Linq;
using System.Text;
using Backoffice.Abstractions.Bo;

namespace Backoffice.Abstractions.PropertyMocks
{
    public class StringProperty
    {
        private string _str;
        
        public StringProperty(string str)
        {
            _str ??= "";
            _str = str;
        }
        
        private static string GetAnonimizedStr(string str)
        {

            const string defaultStr = "*****";

            try
            {
                return str.Length < 3 ? defaultStr : 
                        $"{str.First()}{new StringBuilder().Insert(0, "*", str.Length - 2)}{str.Last()}";
            }
            catch (Exception)
            {
                return defaultStr;
            } 
        }
        
        public string GetValue(IBackOfficeUser user)
        {
            if (string.IsNullOrEmpty(_str))
                return _str;

            if (user == null)
                return GetAnonimizedStr(_str ?? string.Empty);

            if (user.IsAdmin || user.ExposePersonalData)
                return _str;

            return GetAnonimizedStr(_str ?? string.Empty);
        }
        
        public string GetRawValue()
        {
            return _str;
        }

        public override string ToString()
        {
            return GetAnonimizedStr(_str);
        }
    }
}