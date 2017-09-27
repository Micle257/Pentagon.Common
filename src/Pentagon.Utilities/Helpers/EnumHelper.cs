// -----------------------------------------------------------------------
//  <copyright file="EnumHelper.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using Extensions;

    public static class EnumHelper
    {
        public static string GetDescription(this Enum enumItem)
        {
            var attribute = enumItem?.GetItemAttribute<DescriptionAttribute>();
            return attribute?.Description;
        }

        public static IEnumerable<(T, TAttribute)> GetValues<T, TAttribute>()
            where TAttribute : Attribute
        {
            var values = (T[])Enum.GetValues(typeof(T));
            foreach (var value in values)
            {
                var attribute = value.GetItemAttribute<TAttribute>();
                yield return (value, attribute);
            }
        }

        public static bool HasValue(this Enum enumValue, Enum value)
        {
            var realValue = Convert.ToInt32(enumValue);
            var compareValue = Convert.ToInt32(value);
            return (realValue & compareValue) == compareValue;
        }
    }
}