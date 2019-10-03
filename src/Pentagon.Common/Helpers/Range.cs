// -----------------------------------------------------------------------
//  <copyright file="Range.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Helpers
{
    using System;
    using System.Collections.Generic;
    using Extensions;
    using JetBrains.Annotations;

    /// <summary> Represents a range over two values, which define the boundaries. </summary>
    /// <typeparam name="T"> The type of the value. </typeparam>
    [UsedImplicitly]
    public class Range<T> : IRange<T>
        where T : IComparable<T>
    {
        /// <summary> Initializes a new instance of the <see cref="Range{T}" /> class. </summary>
        /// <param name="firstBoundary"> The first boundary. </param>
        /// <param name="secondBoundary"> The second boundary. </param>
        /// <param name="type"> The type or boundaries. </param>
        public Range(T firstBoundary, T secondBoundary, RangeBoundaries type = RangeBoundaries.InIn)
        {
            if (firstBoundary.CompareTo(secondBoundary) <= 0)
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

        /// <summary> Performs an implicit conversion from value tuple to <see cref="Range{T}" />. </summary>
        /// <param name="range"> The range. </param>
        /// <returns> The <see cref="Range{T}" /> result of the conversion. </returns>
        public static implicit operator Range<T>((T min, T max) range) => new Range<T>(range.min, range.max);

        /// <summary> Performs an implicit conversion from value tuple to <see cref="Range{T}" />. </summary>
        /// <param name="range"> The range. </param>
        /// <returns> The <see cref="Range{T}" /> result of the conversion. </returns>
        public static implicit operator Range<T>((T min, T max, RangeBoundaries type) range) => new Range<T>(range.min, range.max, range.type);

        #endregion

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
            var obj = new Range<T>(min, max, type);
            return obj.InRange(value);
        }

        /// <inheritdoc />
        public override string ToString()
            => $"{(BoundariesType.IsAnyEqual(RangeBoundaries.InIn, RangeBoundaries.InOut) ? "[" : "(")}{Min}; {Max}{(BoundariesType.IsAnyEqual(RangeBoundaries.InIn, RangeBoundaries.InOut) ? "]" : ")")}";
    }
}