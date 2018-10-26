// -----------------------------------------------------------------------
//  <copyright file="NumberHelper.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Helpers
{
    using System;

    /// <summary> Provides helper logic for number operations. </summary>
    public static class NumberHelper
    {
        /// <summary> Shifts the and then wrap value by given number of positions using left-shifting. Usage in distinct <see cref="object.GetHashCode" /> combinations. </summary>
        /// <param name="value"> The value. </param>
        /// <param name="positions"> The positions. </param>
        /// <returns> The <see cref="int" />. </returns>
        public static int ShiftAndWrap(int value, int positions)
        {
            positions = positions & 0x1F;

            var number = BitConverter.ToUInt32(BitConverter.GetBytes(value), 0);

            var wrapped = number >> (32 - positions);
            
            return BitConverter.ToInt32(BitConverter.GetBytes((number << positions) | wrapped), 0);
        }

        /// <summary> Shifts the and then wrap value by given number of positions using left-shifting. </summary>
        /// <param name="value"> The value. </param>
        /// <param name="positions"> The positions. </param>
        /// <returns> The <see cref="byte" />. </returns>
        public static byte ShiftAndWrap(byte value, byte positions)
        {
            var wrapped = value >> (8 - positions);

            return BitConverter.GetBytes((value << positions) | wrapped)[0];
        }
    }
}