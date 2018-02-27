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
        public static Type GetNullableType([NotNull] this Type dataType)
        {
            Require.NotNull(() => dataType);

            if (dataType.GetTypeInfo().IsGenericType &&
                dataType.GetGenericTypeDefinition() == typeof(Nullable<>))
                return Nullable.GetUnderlyingType(dataType);

            return null;
        }

        /// <summary> Gets the non static, get/set properties of the type. </summary>
        /// <param name="type"> The type. </param>
        /// <returns> A <see cref="IEnumerable{PropertyInfo}" />. </returns>
        public static IEnumerable<PropertyInfo> GetAutoProperties([NotNull] this Type type)
        {
            return type.GetRuntimeProperties().Where(p => p.CanWrite && p.CanRead && p.GetMethod.IsPublic && p.SetMethod.IsPublic && !p.GetMethod.IsStatic);
        }

        /// <summary>
        /// Gets the value from the field or propertu member info.
        /// </summary>
        /// <param name="memberInfo">The member metadata.</param>
        /// <param name="instance">The instance.</param>
        /// <returns>
        /// A <see cref="object"/> that represents the value; or <c>null</c> if the member is not filed nor property.
        /// </returns>
        public static object GetValue([NotNull] this MemberInfo memberInfo, object instance)
        {
            Require.NotNull(() => memberInfo);
            switch (memberInfo.MemberType)
            {
                case MemberTypes.Field:
                    return ((FieldInfo)memberInfo).GetValue(instance);
                case MemberTypes.Property:
                    return ((PropertyInfo)memberInfo).GetValue(instance);
                default:
                    return null;
            }
        }
    }
}