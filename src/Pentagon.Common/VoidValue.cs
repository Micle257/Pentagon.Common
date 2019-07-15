// -----------------------------------------------------------------------
//  <copyright file="VoidValue.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon
{
    using System;

    /// <summary> Represents a void type, since <see cref="System.Void" /> is not a valid return type in C#. </summary>
    /// <remarks> Can be used as alternative to void or non-generic class override for generic class. </remarks>
    public readonly struct VoidValue : IEquatable<VoidValue>, IComparable<VoidValue>, IComparable
    {
        /// <summary> Default and only value of the <see cref="VoidValue" /> type. </summary>
        public static readonly VoidValue Value = new VoidValue();

        #region Operators

        /// <summary> Implements the operator ==. </summary>
        /// <param name="left"> The left. </param>
        /// <param name="right"> The right. </param>
        /// <returns> The result of the operator. </returns>
        public static bool operator ==(VoidValue left, VoidValue right) => left.Equals(right);

        /// <summary> Implements the operator !=. </summary>
        /// <param name="left"> The left. </param>
        /// <param name="right"> The right. </param>
        /// <returns> The result of the operator. </returns>
        public static bool operator !=(VoidValue left, VoidValue right) => !left.Equals(right);

        #endregion

        /// <summary> Returns a hash code for this instance. </summary>
        /// <returns> A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. </returns>
        public override int GetHashCode() => 0;

        /// <summary> Determines whether the specified <see cref="System.Object" /> is equal to this instance. </summary>
        /// <param name="obj"> The object to compare with the current instance. </param>
        /// <returns> <c> true </c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c> false </c>. </returns>
        public override bool Equals(object obj) => obj is VoidValue;

        /// <summary> Compares the current object with another object of the same type. </summary>
        /// <param name="other"> An object to compare with this object. </param>
        /// <returns>
        ///     <para> A value that indicates the relative order of the objects being compared. </para>
        ///     <para> The return value has the following meanings: </para>
        ///     <list type="bullet">
        ///         <item>
        ///             <description> Less than zero: This object is less than the <paramref name="other" /> parameter. </description>
        ///         </item> <item>
        ///             <description> Zero: This object is equal to <paramref name="other" />. </description>
        ///         </item> <item>
        ///             <description> Greater than zero: This object is greater than <paramref name="other" />. </description>
        ///         </item>
        ///     </list>
        /// </returns>
        public int CompareTo(VoidValue other) => 0;

        /// <summary> Compares the current instance with another object of the same type and returns an integer that indicates whether the current instance precedes, follows, or occurs in the same position in the sort order as the other object. </summary>
        /// <param name="obj"> An object to compare with this instance. </param>
        /// <returns>
        ///     A value that indicates the relative order of the objects being compared. The return value has these meanings: - Less than zero: This instance precedes <paramref
        ///                                                                                                                                                                name="obj" /> in the sort order. - Zero: This instance occurs in the same position in the sort order as <paramref
        ///                                                                                                                                                                                                                                                                            name="obj" />. - Greater than zero: This instance follows <paramref
        ///                                                                                                                                                                                                                                                                                                                                          name="obj" /> in the sort order.
        /// </returns>
        int IComparable.CompareTo(object obj) => 0;

        /// <summary> Determines whether the current object is equal to another object of the same type. </summary>
        /// <param name="other"> An object to compare with this object. </param>
        /// <returns> <c> true </c> if the current object is equal to the <paramref name="other" /> parameter; otherwise, <c> false </c>. </returns>
        public bool Equals(VoidValue other) => true;

        /// <summary> Returns a <see cref="System.String" /> that represents this instance. </summary>
        /// <returns> A <see cref="System.String" /> that represents this instance. </returns>
        public override string ToString() => "(void)";
    }
}