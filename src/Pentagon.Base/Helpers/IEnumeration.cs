// -----------------------------------------------------------------------
//  <copyright file="IEnumeration.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Helpers
{
    using System;

    /// <summary> Represents a custom implementation of <see cref="Enum" />. </summary>
    public interface IEnumeration
    {
        /// <summary> Gets the index of the item. </summary>
        /// <value> The <see cref="int" /> of index. </value>
        int Index { get; }

        /// <summary> Gets the name of the item. </summary>
        /// <value> The <see cref="string" /> of name. </value>
        string Name { get; }
    }
}