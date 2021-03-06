// -----------------------------------------------------------------------
//  <copyright file="RangeCompound.cs">
//   Copyright (c) Michal Pokorn�. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Ranges
{
    using System;
    using System.Linq;
    using JetBrains.Annotations;

    /// <summary> Represents a range with multiple ranges. </summary>
    /// <typeparam name="T"> The type of the value. </typeparam>
    public class RangeCompound<T> : IRange<T>
        where T : IComparable<T>
    {
        /// <summary> The ranges. </summary>
        [NotNull]
        readonly IRange<T>[] _ranges;

        /// <summary> Initializes a new instance of the <see cref="RangeCompound{T}" /> class. </summary>
        /// <param name="ranges"> The ranges. </param>
        public RangeCompound([NotNull] params IRange<T>[] ranges)
        {
            if (ranges == null)
                throw new ArgumentNullException(nameof(ranges));

            if (ranges.Length == 0)
                throw new ArgumentException("Compound range cannot be empty.");

            _ranges = ranges;
        }

        /// <inheritdoc />
        public T Min => _ranges.Min(r => r.Min);

        /// <inheritdoc />
        public T Max => _ranges.Max(r => r.Max);

        /// <inheritdoc />
        public bool InRange(T value)
        {
            return _ranges.Any(range => range.InRange(value));
        }
    }
}