// -----------------------------------------------------------------------
//  <copyright file="CharExtensions.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Extensions
{
    /// <summary> Contains extensions methods for <see cref="char" />. </summary>
    public static class CharExtensions
    {
        /// <summary> Determines whether the character is a hexadecimal digit. </summary>
        /// <param name="value"> The character. </param>
        /// <returns> <c> true </c> if character is hexadecimal digit]; otherwise, <c> false </c>. </returns>
        public static bool IsHexDigit(this char value) => char.IsDigit(c: value) || value.ToString().ToLowerInvariant().IsAnyEqual("a", "b", "c", "d", "e", "f");
    }
}