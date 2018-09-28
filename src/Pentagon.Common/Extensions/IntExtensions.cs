// -----------------------------------------------------------------------
//  <copyright file="IntExtensions.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Extensions
{
    using System;

    /// <summary> Contains extension methods for <see cref="int" />. </summary>
    public static class IntExtensions
    {
        /// <summary> Calculate Mod function of number. </summary>
        /// <param name="a"> Operand. </param>
        /// <param name="mod"> Modulo. </param>
        /// <exception cref="System.DivideByZeroException"> </exception>
        public static int Mod(this int a, int mod)
        {
            if (mod == 0)
                throw new DivideByZeroException(message: "The module cannot be zero.");

            var rem = a % mod;

            if (mod > 0 && rem < 0 || mod < 0 && rem > 0)
                return rem + mod;
            return rem;
        }

        /// <summary> Convert int to string with left padded zeros. </summary>
        /// <param name="value"> The value. </param>
        /// <param name="numberOfDigits"> The number of digits. </param>
        public static string ToFillString(this int value, int numberOfDigits) => value.ToString().PadLeft(numberOfDigits, '0');
    }
}