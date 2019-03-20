// -----------------------------------------------------------------------
//  <copyright file="ReflectionHelper.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using JetBrains.Annotations;

    /// <summary> Represents helper for reflection manipulation. </summary>
    public static class ReflectionHelper
    {
        /// <summary> Gets values from the public constants of a type. </summary>
        /// <typeparam name="T"> The type of the constant. </typeparam>
        /// <param name="type"> The type. </param>
        /// <returns> A read-only list, elements are constant values. </returns>
        [Pure]
        [NotNull]
        public static IReadOnlyList<T> GetAllPublicConstantValues<T>([NotNull] Type type)
        {
            if (type == null)
                throw new ArgumentNullException(nameof(type));

            return type
                   .GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
                   .Where(fi => fi.IsLiteral && !fi.IsInitOnly && fi.FieldType == typeof(T))
                   .Select(x => (T) x.GetRawConstantValue())
                   .ToList();
        }

        /// <summary> Gets public constants of a type. </summary>
        /// <param name="type"> The type. </param>
        /// <returns> A read-only list, elements are <see cref="FieldInfo" />. </returns>
        /// <exception cref="ArgumentNullException"> Throws if
        ///     <param name="type"> </param>
        ///     is null </exception>
        [Pure]
        [NotNull]
        [ItemNotNull]
        public static IReadOnlyList<FieldInfo> GetPublicConstants([NotNull] Type type)
        {
            if (type == null)
                throw new ArgumentNullException(nameof(type));

            return type.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
                       .Where(fi => fi.IsLiteral && !fi.IsInitOnly)
                       .ToList();
        }
    }
}