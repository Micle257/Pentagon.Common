// -----------------------------------------------------------------------
//  <copyright file="TypeExtensions.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using JetBrains.Annotations;

    /// <summary> Contains extension methods for <see cref="Type" />. </summary>
    public static class TypeExtensions
    {
        /// <summary> Gets the generic type of the nullable. </summary>
        /// <param name="dataType"> Type of the data. </param>
        /// <returns> The <see cref="Type" /> of generic, if failed <c> null </c>. </returns>
        public static Type GetNullableType(this Type dataType)
        {
            if (dataType == null)
                return null;

            if (dataType.GetTypeInfo().IsGenericType &&
                dataType.GetGenericTypeDefinition() == typeof(Nullable<>))
                dataType = Nullable.GetUnderlyingType(dataType);

            return dataType;
        }

        /// <summary> Gets the non static, get/set properties of the type. </summary>
        /// <param name="type"> The type. </param>
        /// <returns> A <see cref="IEnumerable{PropertyInfo}" />. </returns>
        public static IEnumerable<PropertyInfo> GetAutoProperties([NotNull] this Type type)
        {
            return type.GetRuntimeProperties().Where(p => p.CanWrite && p.CanRead && p.GetMethod.IsPublic && p.SetMethod.IsPublic && !p.GetMethod.IsStatic);
        }
    }
}