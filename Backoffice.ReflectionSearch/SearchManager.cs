using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Backoffice.ReflectionSearch
{
    public enum ValueInputsTypes
    {
        Disabled,
        StringValue,
        Multiselect,
        SingleSelect
    }

    public static class SearchManager
    {
        public static SearchModel GenerateSearchModel(this IEnumerable<object> src)
        {
            if (src == null)
                throw new Exception("Source list is null.");

            var searchProperties = src.FirstOrDefault()?
                .GetType().GetProperties()
                .Where(prop => Attribute.IsDefined(prop, typeof(SearchableAttribute)))
                .ToList();

            if (searchProperties == null)
                return SearchModel.Create(Array.Empty<SearchModelItem>());

            var searchItems =
                searchProperties.Select(prop =>
                        SearchModelItem.Create(prop.GetPropertySearchType(),
                            prop.PropertyType,
                            src.Select(itm => prop.GetValue(itm)?.ToString()),
                            prop.Name))
                    .ToList();

            return SearchModel.Create(searchItems);
        }

        public static IEnumerable<object> FilterModels(this IEnumerable<object> src,
            IEnumerable<SearchFilterItem> filters)
        {
            var searchProperties = src.FirstOrDefault()?
                .GetType().GetProperties()
                .Where(prop => Attribute.IsDefined(prop, typeof(SearchableAttribute)))
                .ToList();

            if (src == null)
                throw new Exception("Source list is null.");

            if (searchProperties == null)
                throw new Exception("Search properties is null.");

            return src.Where(itm =>
            {
                var isValid = false;

                foreach (var filterItem in filters)
                {
                    var targetProperty = searchProperties
                        .FirstOrDefault(itm => itm.Name == filterItem.PropertyName);

                    if (targetProperty == null)
                    {
                        continue;
                    }

                    var targetValue = targetProperty.GetValue(itm);
                    var condition = Enum.Parse<Conditions>(filterItem.Condition);

                    if (condition != Conditions.NotEmpty && condition != Conditions.IsEmpty &&
                        condition != Conditions.NotIn && targetValue == null)
                    {
                        continue;
                    }

                    switch (condition)
                    {
                        case Conditions.EqualsTo:
                            isValid = filterItem.FilterValues.FirstOrDefault()?.Equals(targetValue) ?? false;
                            break;
                        case Conditions.Contains:
                            isValid = ((string) targetValue)?.Contains(filterItem.FilterValue) ?? false;
                            break;
                        case Conditions.NotIn:
                            isValid = !filterItem.FilterValues.Contains((string) targetValue);
                            break;
                        case Conditions.Less:
                        {
                            if (targetProperty.PropertyType == typeof(DateTime))
                            {
                                isValid = (DateTime) targetValue < DateTime.Parse(filterItem.FilterValue);
                                break;
                            }

                            isValid = (int) targetValue < (int) targetValue;
                            break;
                        }
                        case Conditions.Bigger:
                        {
                            if (targetProperty.PropertyType == typeof(DateTime))
                            {
                                isValid = (DateTime) targetValue > DateTime.Parse(filterItem.FilterValue);
                                break;
                            }

                            isValid = (int) targetValue > (int) targetValue;
                            break;
                        }
                        case Conditions.IsEmpty:
                            isValid = targetValue == null;
                            break;
                        case Conditions.In:
                            isValid = filterItem.FilterValues.Contains(targetValue.ToString());
                            break;
                        case Conditions.NotEmpty:
                            isValid = targetValue != null;
                            break;
                        case Conditions.IsTrue:
                            isValid = (bool) targetValue;
                            break;
                        case Conditions.IsFalse:
                            isValid = (bool) targetValue == false;
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }


                    if (!isValid)
                    {
                        break;
                    }
                }

                return isValid;
            });
        }

        private static SearchFieldTypes GetPropertySearchType(this PropertyInfo src)
        {
            if (src.PropertyType == typeof(string))
            {
                return SearchFieldTypes.String;
            }

            if (src.PropertyType == typeof(bool))
            {
                return SearchFieldTypes.Bool;
            }

            if (src.PropertyType == typeof(short))
            {
                return SearchFieldTypes.Numeric;
            }

            if (src.PropertyType == typeof(ushort))
            {
                return SearchFieldTypes.Numeric;
            }

            if (src.PropertyType == typeof(int))
            {
                return SearchFieldTypes.Numeric;
            }

            if (src.PropertyType == typeof(uint))
            {
                return SearchFieldTypes.Numeric;
            }

            if (src.PropertyType == typeof(long))
            {
                return SearchFieldTypes.Numeric;
            }

            if (src.PropertyType == typeof(ulong))
            {
                return SearchFieldTypes.Numeric;
            }

            if (src.PropertyType == typeof(float))
            {
                return SearchFieldTypes.Numeric;
            }

            if (src.PropertyType == typeof(double))
            {
                return SearchFieldTypes.Numeric;
            }

            if (src.PropertyType == typeof(decimal))
            {
                return SearchFieldTypes.Numeric;
            }

            if (src.PropertyType == typeof(DateTime))
            {
                return SearchFieldTypes.Date;
            }

            throw new Exception($"Cant parse type: {src.PropertyType} for search.");
        }

        public static IEnumerable<string> GetConditionsByFieldType(this SearchFieldTypes src)
        {
            return src switch
            {
                SearchFieldTypes.Bool => Enum.GetNames(typeof(BoolConditions)),
                SearchFieldTypes.String => Enum.GetNames(typeof(StringConditions)),
                SearchFieldTypes.Numeric => Enum.GetNames(typeof(NumericConditions)),
                SearchFieldTypes.Date => Enum.GetNames(typeof(DateConditions)),
                _ => throw new ArgumentOutOfRangeException(nameof(src), src, null)
            };
        }

        public static ValueInputsTypes GetValueInputType(this string selectedCondition)
        {
            switch (selectedCondition)
            {
                case "IsEmpty":
                case "NotEmpty":
                case "IsTrue":
                case "IsFalse":
                case "":
                    return ValueInputsTypes.Disabled;
                case "Contains":
                    return ValueInputsTypes.StringValue;
                case "Less":
                case "Bigger":
                case "Equals":
                    return ValueInputsTypes.SingleSelect;
                default:
                    return ValueInputsTypes.Multiselect;
            }
        }

        public static string PrettyJson(object unPrettyJson)
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };

            var jsonElement = JsonSerializer.Deserialize<JsonElement>(JsonConvert.SerializeObject(unPrettyJson));

            return JsonSerializer.Serialize(jsonElement, options);
        }
    }
}