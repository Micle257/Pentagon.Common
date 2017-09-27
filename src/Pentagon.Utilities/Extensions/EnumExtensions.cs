// -----------------------------------------------------------------------
//  <copyright file="EnumExtensions.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------
namespace Pentagon.Extensions
{
    using System;
    using System.Reflection;
    using JetBrains.Annotations;

    /// <summary>
    /// Contains extension methods for <see cref="Enum" />.
    /// </summary>
    public static class EnumExtensions
    {
        /// <summary>
        /// Gets an attribute on an enum field value.
        /// </summary>
        /// <typeparam name="TAttribute">The type of the attribute on the enum value.</typeparam>
        /// <param name="enumValueBox">The enum value.</param>
        /// <returns>
        /// A <see cref="TAttribute"/> that is on the enum value or <c>null</c>.
        /// </returns>
        public static TAttribute GetItemAttribute<TAttribute>([NotNull] this object enumValueBox)
            where TAttribute : Attribute
        {
            Require.NotNull(() => enumValueBox);
            Require.IsType(() => enumValueBox, out Enum enumValue);
            var type = enumValue.GetType();
            var memberInfos = type.GetTypeInfo()?.GetMember(Enum.GetName(type, enumValue));
            if (memberInfos == null || memberInfos.Length <= 0)
                throw new ArgumentException(message: "The enumValue was not found.");
            var attribute = memberInfos[0].GetCustomAttribute<TAttribute>();
            return attribute;
        }
    }
}