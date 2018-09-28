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
    using System.Linq;
    using Extensions;
    using JetBrains.Annotations;

    /// <summary> Represents helper for enum and enum items. </summary>
    public static class EnumHelper
    {
        /// <summary> Gets the description from enum item with <see cref="DescriptionAttribute" />. </summary>
        /// <param name="enumItem"> The enum item. </param>
        /// <returns> A <see cref="String" /> representing description, -or- <c> null </c> if description attribute is not present. </returns>
        public static string GetDescription(this Enum enumItem)
        {
            var attribute = enumItem?.GetItemAttribute<DescriptionAttribute>();
            return attribute?.Description;
        }

        /// <summary> Gets the attribute values from enum items. </summary>
        /// <typeparam name="T"> The type of the enum. </typeparam>
        /// <typeparam name="TAttribute"> The type of the attribute. </typeparam>
        /// <returns> An enumerable of value pair of <see cref="T" /> and <see cref="TAttribute" />. </returns>
        [NotNull]
        public static IEnumerable<(T, TAttribute)> GetValues<T, TAttribute>()
                where TAttribute : Attribute
        {
            var values = (T[]) Enum.GetValues(typeof(T));

            foreach (var value in values)
            {
                var attribute = value.GetItemAttribute<TAttribute>();
                yield return (value, attribute);
            }
        }

        /// <summary> Determines whether the specified enum item contains other enum item, used in flag enum. </summary>
        /// <param name="enumValue"> The enum item. </param>
        /// <param name="value"> The other enum item. </param>
        /// <returns> <c> true </c> if the specified value has value; otherwise, <c> false </c>. </returns>
        public static bool HasValue(this Enum enumValue, Enum value)
        {
            var realValue = Convert.ToInt32(enumValue);
            var compareValue = Convert.ToInt32(value);
            return (realValue & compareValue) == compareValue;
        }

        public static TEnum ConvertFromString<TEnum>(string value)
        {
            if (value == null)
                return default;

            if (string.IsNullOrEmpty(value))
                return default;

            try
            {
                var result = GetValues<TEnum, DescriptionAttribute>()
                             .FirstOrDefault(e => e.Item2?.Description == value).Item1;

                return result;
            }
            catch (Exception e)
            {
                return default;
            }
        }

        public static string ConvertToString<TEnum>(TEnum value)
        {
            try
            {
                var result = GetValues<TEnum, DescriptionAttribute>()
                             .FirstOrDefault(e => Enum.GetName(typeof(TEnum), value) == Enum.GetName(typeof(TEnum), e.Item1)).Item2;

                return result.Description;
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}