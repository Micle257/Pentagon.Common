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