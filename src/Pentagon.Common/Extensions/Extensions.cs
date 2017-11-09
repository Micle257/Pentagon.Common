// -----------------------------------------------------------------------
//  <copyright file="Extensions.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Extensions
{
    using JetBrains.Annotations;

    /// <summary> Contains general extension methods. </summary>
    public static class Extensions
    {
        /// <summary> Determines whether value data type is default. </summary>
        /// <typeparam name="T"> Type of the value type. </typeparam>
        /// <param name="value"> The value. </param>
        /// <returns> <c> true </c> if the specified value is default; otherwise, <c> false </c>. </returns>
        public static bool IsDefault<T>(this IValueDataType<T> value)
            where T : IValueDataType<T>
            => value?.HasValue ?? false;

        /// <summary> Determines if given object is equal to any of parameters </summary>
        /// <typeparam name="T"> Type of value. </typeparam>
        /// <param name="value"> Modified object </param>
        /// <param name="pars"> Array of objects </param>
        /// <returns> </returns>
        public static bool IsAnyEqual<T>(this T value, [NotNull] params T[] pars)
        {
            Require.NotNull(() => pars);
            Require.Condition(() => pars, v => v.Length > 0, message: "Value cannot be an empty collection.");

            foreach (var item in pars)
            {
                if (ReferenceEquals(value, item))
                    return true;
                if (value.Equals(item))
                    return true;
            }
            return false;
        }
    }
}