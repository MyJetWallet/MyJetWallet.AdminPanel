using System;
using System.Collections.Generic;

namespace Backoffice.Services
{
    public static class EnumUtils
    {
        public static string JoinIntoString<T>(this IEnumerable<T> values, int maxLength)
        {
            var resultCollection = new List<string>();
            var length = 0;
            foreach (var value in values)
            {
                var stringValue = value.ToString();
                if (stringValue != null)
                {
                    length += stringValue.Length + 2;
                    if (length < maxLength)
                    {
                        resultCollection.Add(stringValue);
                    }
                    else
                    {
                        break;
                    }
                }
            }

            return string.Join(", ", resultCollection);
        }

        public static TEnum ToEnum<TEnum>(this string stringValue) where TEnum : struct
        {
            if (string.IsNullOrEmpty(stringValue) || !Enum.TryParse(stringValue, true, out TEnum enumValue))
            {
                return default;
            }

            return enumValue;
        }
    }
}
