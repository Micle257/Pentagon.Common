﻿// -----------------------------------------------------------------------
//  <copyright file="EnumExtensions.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Extensions
{
    using System;
    using System.Reflection;
    using JetBrains.Annotations;

    /// <summary> Contains extension methods for <see cref="Enum" />. </summary>
    public static class EnumExtensions
    {
        /// <summary>
        /// Gets an attribute on an enum field value.
        /// </summary>
        /// <typeparam name="TAttribute">The type of the attribute on the enum value.</typeparam>
        /// <typeparam name="TEnum">The type of the enum.</typeparam>
        /// <param name="value">The enum value.</param>
        /// <returns>
        /// A <see cref="TAttribute" /> that is on the enum value or <c> null </c>.
        /// </returns>
        public static TAttribute GetItemAttribute<TAttribute, TEnum>([NotNull] this TEnum value)
            where TAttribute : Attribute
            where TEnum : Enum
        {
            var type = value.GetType();
            var memberInfos = type.GetTypeInfo()?.GetMember(Enum.GetName(type, value));
            if (memberInfos == null || memberInfos.Length <= 0)
                throw new ArgumentException(message: "The enumValue was not found.");
            var attribute = memberInfos[0].GetCustomAttribute<TAttribute>();
            return attribute;
        }
    }
}