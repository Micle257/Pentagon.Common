// -----------------------------------------------------------------------
//  <copyright file="Range.cs">
//   Copyright (c) Michal Pokorn�. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Ranges
{
    using System;
    using System.Collections.Generic;
    using Extensions;
    using JetBrains.Annotations;

    /// <summary> Represents a range over two values, which define the boundaries. </summary>
    /// <typeparam name="T"> The type of the value. </typeparam>
    [UsedImplicitly]
    public class Range<T> : IRange<T>, IEquatable<Range<T>>
            where T : IComparable<T>
    {
        /// <summary> Initializes a new instance of the <see cref="Range{T}" /> class. </summary>
        /// <param name="firstBoundary"> The first boundary. </param>
        /// <param name="secondBoundary"> The second boundary. </param>
        /// <param name="type"> The type or boundaries. </param>
        public Range(T firstBoundary, T secondBoundary, RangeBoundaries type = RangeBoundaries.InIn)
        {
            if (firstBoundary.CompareTo(other: secondBoundary) <= 0)
            {
                Min = firstBoundary;
                Max = secondBoundary;
            }
            else
            {
                Min = secondBoundary;
                Max = firstBoundary;
            }

            BoundariesType = type;
        }

        /// <summary> Gets the type of the boundaries. </summary>
        /// <value> The <see cref="RangeBoundaries" /> type. </value>
        public RangeBoundaries BoundariesType { get; }

        /// <inheritdoc />
        public T Min { get; }

        /// <inheritdoc />
        public T Max { get; }

        #region Operators

        public static bool operator ==(Range<T> left, Range<T> right) => Equals(objA: left, objB: right);

        public static bool operator !=(Range<T> left, Range<T> right) => !Equals(objA: left, objB: right);

        /// <summary> Performs an implicit conversion from value tuple to <see cref="Range{T}" />. </summary>
        /// <param name="range"> The range. </param>
        /// <returns> The <see cref="Range{T}" /> result of the conversion. </returns>
        public static implicit operator Range<T>((T min, T max) range) => new Range<T>(firstBoundary: range.min, secondBoundary: range.max);

        /// <summary> Performs an implicit conversion from value tuple to <see cref="Range{T}" />. </summary>
        /// <param name="range"> The range. </param>
        /// <returns> The <see cref="Range{T}" /> result of the conversion. </returns>
        public static implicit operator Range<T>((T min, T max, RangeBoundaries type) range) => new Range<T>(firstBoundary: range.min, secondBoundary: range.max, type: range.type);

        #endregion

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, objB: obj))
                return false;
            if (ReferenceEquals(this, objB: obj))
                return true;

            return obj.GetType() == GetType() && Equals((Range<T>) obj);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (int) BoundariesType;
                hashCode = (hashCode * 397) ^ EqualityComparer<T>.Default.GetHashCode(obj: Min);
                hashCode = (hashCode * 397) ^ EqualityComparer<T>.Default.GetHashCode(obj: Max);
                return hashCode;
            }
        }

        /// <inheritdoc />
        public bool Equals(Range<T> other)
        {
            if (ReferenceEquals(null, objB: other))
                return false;
            if (ReferenceEquals(this, objB: other))
                return true;

            return BoundariesType == other.BoundariesType && EqualityComparer<T>.Default.Equals(x: Min, y: other.Min) && EqualityComparer<T>.Default.Equals(x: Max, y: other.Max);
        }

        /// <inheritdoc />
        public virtual bool InRange(T value)
        {
            var min = Min;
            var max = Max;

            var list = new List<T> {min, max};
            list.Sort();

            var compare1 = value.CompareTo(list[0]);
            var compare2 = value.CompareTo(list[1]);

            switch (BoundariesType)
            {
                case RangeBoundaries.InIn:
                    return compare1 >= 0 && compare2 <= 0;
                case RangeBoundaries.OutOut:
                    return compare1 > 0 && compare2 < 0;
                case RangeBoundaries.InOut:
                    return compare1 >= 0 && compare2 < 0;
                case RangeBoundaries.OutIn:
                    return compare1 > 0 && compare2 <= 0;
                default:
                    return false;
            }
        }

        /// <summary> Determines if given values is inside boundaries. </summary>
        /// <param name="value"> The value. </param>
        /// <param name="min"> The down boundary. </param>
        /// <param name="max"> The up boundary. </param>
        /// <param name="type"> The type of bounties. </param>
        /// <returns> A <c> true </c> if value is in range; otherwise <c> false </c>. </returns>
        public static bool InRange(in T value, T min, T max, RangeBoundaries type)
        {
            var obj = new Range<T>(firstBoundary: min, secondBoundary: max, type: type);
            return obj.InRange(value: value);
        }

        /// <inheritdoc />
        public override string ToString()
            => $"{(BoundariesType.IsAnyEqual(RangeBoundaries.InIn, RangeBoundaries.InOut) ? "[" : "(")}{Min}; {Max}{(BoundariesType.IsAnyEqual(RangeBoundaries.InIn, RangeBoundaries.InOut) ? "]" : ")")}";
    }
}