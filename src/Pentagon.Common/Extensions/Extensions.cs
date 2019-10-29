// -----------------------------------------------------------------------
//  <copyright file="Extensions.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Extensions
{
    using System;
    using System.Collections.Generic;
    using JetBrains.Annotations;

    /// <summary> Contains general extension methods. </summary>
    public static class Extensions
    {
        /// <summary> Determines if given object is equal to any of parameters </summary>
        /// <typeparam name="T"> Type of value. </typeparam>
        /// <param name="value"> Modified object </param>
        /// <param name="pars"> Array of objects </param>
        /// <returns>Value indicating whether equal value was found in parameters.</returns>
        public static bool IsAnyEqual<T>(this T value, [NotNull] params T[] pars)
        {
            if (pars == null)
                throw new ArgumentNullException(nameof(pars));

            if (pars.Length == 0)
                throw new ArgumentException("Value cannot be an empty collection.");

            foreach (var item in pars)
            {
                if (ReferenceEquals(value, item))
                    return true;

                if (EqualityComparer<T>.Default.Equals(value, item))
                    return true;
            }

            return false;
        }
    }
}