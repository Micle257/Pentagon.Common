// -----------------------------------------------------------------------
//  <copyright file="DoubleExtensions.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Extensions
{
    using System;

    /// <summary> Contains extension methods for <see cref="double" />. </summary>
    public static class DoubleExtensions
    {
        /// <summary> The default precision for comparing float number based values. </summary>
        internal const double DefaultPrecision = 1e-15d;

        /// <summary> Calculate Mod function of number. </summary>
        /// <param name="a"> The operand. </param>
        /// <param name="mod"> The modulo. </param>
        /// <returns> A <see cref="double" />. </returns>
        /// <exception cref="System.DivideByZeroException"> <paramref name="mod" /> is equal to zero. </exception>
        public static double Mod(this double a, int mod)
        {
            if (mod == 0)
                throw new DivideByZeroException();

            var rem = a % mod;

            if (mod > 0 && rem < 0 || mod < 0 && rem > 0)
                return rem + mod;
            return rem;
        }

        /// <summary> Compare to another <see cref="double" /> with default precision. </summary>
        /// <param name="a"> The value of first <see cref="double" />. </param>
        /// <param name="b"> The value of second <see cref="double" />. </param>
        /// <returns> <c> true </c> if value is equal to other value; otherwise, <c> false </c>. </returns>
        public static bool EqualTo(this double a, double b) => a.EqualTo(b, DefaultPrecision);

        /// <summary> Compare to another <see cref="double" /> with certain precision. </summary>
        /// <param name="a"> The value of first <see cref="double" />. </param>
        /// <param name="b"> The value of second <see cref="double" />. </param>
        /// <param name="precision"> The comparing precision. </param>
        /// <returns> <c> true </c> if value is equal to other value with tolerance; otherwise, <c> false </c>. </returns>
        public static bool EqualTo(this double a, double b, double precision) => Math.Abs(a - b) <= precision;

        /// <summary> Gets the value rounded to significant digits. </summary>
        /// <param name="d"> Value to round. </param>
        /// <param name="digits"> Number of the significant figures. </param>
        /// <returns> A <see cref="double" />. </returns>
        public static double RoundSignificantFigures(this double d, int digits)
        {
            if (digits <= 0)
                throw new ArgumentException(message: "Significant digits must be greaten than zero.", paramName: nameof(digits));

            if (d.EqualTo(0, 10e-250d))
                return 0;

            var scale = (decimal) Math.Pow(10, Math.Floor(Math.Log10(Math.Abs(d))) + 1);

            return (double) (scale * Math.Round((decimal) d / scale, digits));
        }
    }
}