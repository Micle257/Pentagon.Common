// -----------------------------------------------------------------------
//  <copyright file="MathInterval.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Helpers
{
    using System;
    using System.Collections.Generic;
    using Extensions;
    using JetBrains.Annotations;

    /// <summary> Represents an two dimensional interval. </summary>
    public class MathInterval : Range<double>
    {
        /// <summary> Initializes a new instance of the <see cref="MathInterval" /> class. </summary>
        /// <param name="min"> The minimum. </param>
        /// <param name="max"> The maximum. </param>
        /// <param name="type"> The type of boundaries. </param>
        public MathInterval(double min, double max, RangeBoundaries type = RangeBoundaries.InIn) : base(min, max, type) { }

        /// <summary> Gets the absolute size of the range. </summary>
        /// <value> The <see cref="double" />. </value>
        public double Size => Math.Abs(Max - Min);

        /// <summary> Gets or sets the precision for numeric types with floating decimal point. </summary>
        /// <value> The <see cref="Int32" />, default is 7 decimal places. </value>
        public int Precision { get; set; } = 7;

        #region Operators

        /// <summary> Performs an implicit conversion from value tuple to <see cref="MathInterval" />. </summary>
        /// <param name="interval"> The interval. </param>
        /// <returns> The result of the conversion. </returns>
        public static implicit operator MathInterval((double min, double max) interval) => new MathInterval(interval.min, interval.max);

        /// <summary> Performs an implicit conversion from value tuple to <see cref="MathInterval" />. </summary>
        /// <param name="interval"> The interval. </param>
        /// <returns> The result of the conversion. </returns>
        public static implicit operator MathInterval((double min, double max, RangeBoundaries type) interval) => new MathInterval(interval.min, interval.max, interval.type);

        #endregion

        /// <inheritdoc />
        public override bool InRange(double v)
        {
            var value = Math.Round(v, Precision);
            var minValue = Math.Round(Min, Precision);
            var maxValue = Math.Round(Max, Precision);

            if (minValue.EqualTo(maxValue, Precision))
                return value.EqualTo(minValue, Precision);

            var list = new List<double> {minValue, maxValue};
            list.Sort();

            switch (BoundariesType)
            {
                case RangeBoundaries.InIn:
                    return value >= list[0] && value <= list[1];
                case RangeBoundaries.OutOut:
                    return value > list[0] && value < list[1];
                case RangeBoundaries.InOut:
                    return value >= list[0] && value < list[1];
                case RangeBoundaries.OutIn:
                    return value > list[0] && value <= list[1];
                default:
                    return false;
            }
        }

        /// <summary> Divides this range into collection given by length of an element. </summary>
        /// <param name="lengthOfElement"> The length of element. </param>
        /// <returns> An <see cref="IEnumerable{T}" /> of divided interval. </returns>
        [NotNull]
        public IEnumerable<double> GetSampledInterval(double lengthOfElement)
        {
            for (var i = Min; i < Max; i += lengthOfElement)
                yield return i;
        }
    }
}