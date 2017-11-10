// -----------------------------------------------------------------------
//  <copyright file="DoubleExtensions.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Extensions
{
    using System;
    using System.Numerics;

    /// <summary> Contains extension methods for <see cref="double" />. </summary>
    public static class DoubleExtensions
    {
        /// <summary> The default precision for comparing float number based values. </summary>
        const double DefaultPrecision = .000001;

        /// <summary> Calculate Mod function of number. </summary>
        /// <param name="a"> Operand. </param>
        /// <param name="mod"> Modulo. </param>
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
        public static bool EqualTo(this double a, double b) => a.EqualTo(b, DefaultPrecision);

        /// <summary> Compare to another <see cref="double" /> with certain precision. </summary>
        /// <param name="a"> The value of first <see cref="double" />. </param>
        /// <param name="b"> The value of second <see cref="double" />. </param>
        /// <param name="precision"> The comparing precision. </param>
        public static bool EqualTo(this double a, double b, double precision) => Math.Abs(a - b) <= precision;

        /// <summary> Compare to another <see cref="Complex" /> with certain precision. </summary>
        /// <param name="a"> The value of first <see cref="Complex" />. </param>
        /// <param name="b"> The value of second <see cref="Complex" />. </param>
        /// <param name="precision"> The comparing precision. </param>
        public static bool EqualTo(this Complex a, Complex b, double precision) => Math.Abs(a.Real - b.Real) <= precision && Math.Abs(a.Imaginary - b.Imaginary) <= precision;

        /// <summary> Compare to another <see cref="Complex" /> with default precision. </summary>
        /// <param name="a"> The value of first <see cref="Complex" />. </param>
        /// <param name="b"> The value of second <see cref="Complex" />. </param>
        public static bool EqualTo(this Complex a, Complex b) => a.EqualTo(b, DefaultPrecision);

        /// <summary> Gets the value rounded to significant digits. </summary>
        /// <param name="d"> Value to round. </param>
        /// <param name="digits"> Number of the significant figures. </param>
        /// <returns> Rounded value. </returns>
        public static double SignificantFigures(this double d, int digits)
        {
            if (d.EqualTo(0))
                return 0;

            var scale = (decimal) Math.Pow(10, Math.Floor(Math.Log10(Math.Abs(d))) + 1);

            return (double) (scale * Math.Round((decimal) d / scale, digits));
        }
    }
}