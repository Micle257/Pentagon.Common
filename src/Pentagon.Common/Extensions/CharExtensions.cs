// -----------------------------------------------------------------------
//  <copyright file="CharExtensions.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Extensions
{
    public static class CharExtensions
    {
        public static bool IsHexDigit(this char value) => char.IsDigit(value) || value.ToString().ToLowerInvariant().IsAnyEqual("a", "b", "c", "d", "e", "f");
    }
}