// -----------------------------------------------------------------------
//  <copyright file="ComplexExtensions.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Extensions
{
    using System;
    using System.Numerics;

    /// <summary> Contains extension methods for <see cref="Complex" />. </summary>
    public static class ComplexExtensions
    {
        /// <summary> Compare to another <see cref="Complex" /> with certain precision. </summary>
        /// <param name="a"> The value of first <see cref="Complex" />. </param>
        /// <param name="b"> The value of second <see cref="Complex" />. </param>
        /// <param name="precision"> The comparing precision. </param>
        /// <returns> <c> true </c> if value is equal to other value with tolerance; otherwise, <c> false </c>. </returns>
        public static bool EqualTo(this Complex a, Complex b, double precision) => Math.Abs(a.Real - b.Real) <= precision && Math.Abs(a.Imaginary - b.Imaginary) <= precision;

        /// <summary> Compare to another <see cref="Complex" /> with default precision. </summary>
        /// <param name="a"> The value of first <see cref="Complex" />. </param>
        /// <param name="b"> The value of second <see cref="Complex" />. </param>
        /// <returns> <c> true </c> if value is equal to other value; otherwise, <c> false </c>. </returns>
        public static bool EqualTo(this Complex a, Complex b) => a.EqualTo(b, DoubleExtensions.DefaultPrecision);
    }
}