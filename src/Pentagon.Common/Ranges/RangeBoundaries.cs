// -----------------------------------------------------------------------
//  <copyright file="RangeBoundaries.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Ranges
{
    /// <summary> Specifies the boundaries of a <see cref="Range{T}" />. </summary>
    public enum RangeBoundaries
    {
        /// <summary> The unspecified, default value. </summary>
        Unspecified,

        /// <summary> The value which includes both boundaries of the range. </summary>
        InIn,

        /// <summary> The value which exclude both boundaries of the range. </summary>
        OutOut,

        /// <summary> The value which includes right boundary and excludes left bounty of the range. </summary>
        InOut,

        /// <summary> The value which excludes right boundary and includes left bounty of the range. </summary>
        OutIn
    }
}