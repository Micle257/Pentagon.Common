// -----------------------------------------------------------------------
//  <copyright file="StringExtensions.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Extensions
{
    using System.Collections.Generic;
    using System.IO;
    using JetBrains.Annotations;

    /// <summary> Contains extension methods for <see cref="System.String" />. </summary>
    public static class StringExtensions
    {
        /// <summary> Surrounds the <see cref="string" /> with quotes. </summary>
        /// <param name="value"> Modified <see cref="string" />. </param>
        public static string Quote(this string value) => "'" + value + "'";

        /// <summary> Converts the <see cref="string" /> representation of a number as an <see cref="int" />. Equivalent to TryParse method. </summary>
        /// <param name="value"> String value to convert. </param>
        /// <returns> <see cref="int" /> value from <paramref name="value" /> string value if represents an integer; otherwise zero. </returns>
        public static int? ToInt(this string value)
        {
            var parseResult = int.TryParse(value, out var a);

            if (!parseResult)
                return null;

            return a;
        }

        /// <summary>
        /// Splits the text into lines.
        /// </summary>
        /// <remarks>Uses <see cref="StreamReader"/> for the process.</remarks>
        /// <param name="input">The input.</param>
        /// <returns>An iteration of <see cref="string"/>, each element represents a line of text.</returns>
        public static IEnumerable<string> SplitToLines([CanBeNull] this string input)
        {
            if (input == null)
                yield break;

            using (var reader = new StringReader(input))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                    yield return line;
            }
        }
    }
}