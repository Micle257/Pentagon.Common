// -----------------------------------------------------------------------
//  <copyright file="StringExtensions.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Extensions
{
    /// <summary> Contains extension methods for <see cref="System.String" />. </summary>
    public static class StringExtensions
    {
        /// <summary> Surrounds the <see cref="string" /> with quotes. </summary>
        /// <param name="value"> Modified <see cref="string" />. </param>
        public static string Quote(this string value) => "'" + value + "'";

        /// <summary> Converts the <see cref="string" /> representation of a number as an <see cref="int" />. Equivalent to TryParse method. </summary>
        /// <param name="value"> String value to convert. </param>
        /// <returns>
        ///     <see cref="int" /> value from <paramref name="value" /> string value if represents an integer; otherwise zero. </returns>
        public static int ToInt(this string value)
        {
            int.TryParse(value, out int a);
            return a;
        }
    }
}