using System;
using System.Collections.Generic;
using System.Linq;

namespace Backoffice.ReflectionSearch
{
    public enum Conditions
    {
        EqualsTo,
        Contains,
        NotIn,
        Less,
        Bigger,
        IsEmpty,
        In,
        NotEmpty,
        IsTrue,
        IsFalse
    }
    
    public enum SearchFieldTypes
    {
        String,
        Numeric,
        Bool,
        Date
    }

    public enum StringConditions
    {
        EqualsTo = Conditions.EqualsTo,
        Contains = Conditions.Contains,
        IsEmpty = Conditions.IsEmpty,
        In = Conditions.In,
        NotIn = Conditions.NotIn,
        NotEmpty = Conditions.NotEmpty
    }

    public enum NumericConditions
    {
        EqualsTo = Conditions.EqualsTo,
        Less = Conditions.Less,
        Bigger = Conditions.Bigger,
        IsEmpty = Conditions.IsEmpty,
        In = Conditions.In,
        NotEmpty = Conditions.NotEmpty
    }

    public enum BoolConditions
    {
        IsTrue = Conditions.IsTrue,
        IsFalse = Conditions.IsFalse
    }

    public enum DateConditions
    {
        In = Conditions.In,
        Less = Conditions.Less,
        Bigger = Conditions.Bigger
    }

    public class SearchFilterItem
    {
        public Guid GuidField = Guid.NewGuid();
        public string PropertyName { get; set; }
        public string Condition { get; set; }
        public string FilterValue { get; set; }
        public IList<string> FilterValues = new List<string>();

        public bool IsFilled()
        {
            return (!string.IsNullOrEmpty(FilterValue) || Condition.GetValueInputType() ==
                    ValueInputsTypes.Disabled) || FilterValues.Any() && PropertyName != null && Condition != null;
        }
    }

    public class SearchFilterModel
    {
        public IEnumerable<SearchFilterItem> FilterModels { get; set; }

        public static SearchFilterModel Create(IEnumerable<SearchFilterItem> models)
        {
            return new()
            {
                FilterModels = models
            };
        }
    }


    public class SearchModelItem
    {
        public SearchFieldTypes Type { get; set; }
        public HashSet<string> PossibleValues { get; set; }
        public string PropertyName { get; set; }
        public Type SourceType { get; set; }

        public static SearchModelItem Create(SearchFieldTypes type, Type sourceType, IEnumerable<string> possibleValues,
            string propertyName)
        {
            return new()
            {
                Type = type,
                SourceType = sourceType,
                PossibleValues = new HashSet<string>(possibleValues),
                PropertyName = propertyName
            };
        }
    }

    public class SearchModel
    {
        public IEnumerable<SearchModelItem> SearchModelItems { get; set; }

        public static SearchModel Create(IEnumerable<SearchModelItem> searchItems)
        {
            return new()
            {
                SearchModelItems = searchItems
            };
        }
    }
}