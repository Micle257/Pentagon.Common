// -----------------------------------------------------------------------
//  <copyright file="RangeTests.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------
// ReSharper disable ExceptionNotDocumented
namespace Pentagon.Common.Tests
{
    using System;
    using Helpers;
    using Xunit;

    public class RangeCompoundTests
    {
        [Fact]
        public void Constructor_RangesParameterIsNull_Throws()
        {
            // ReSharper disable once AssignNullToNotNullAttribute
            Assert.Throws<ArgumentNullException>(() => new RangeCompound<byte>(null));
        }

        [Fact]
        public void Constructor_RangeArrayIsEmpty_Throws()
        {
            Assert.Throws<ArgumentException>(() => new RangeCompound<byte>());
        }

        [Fact]
        public void Min_ReturnsMinValue()
        {
            var range = new RangeCompound<sbyte>(new Range<sbyte>(0,2), new Range<sbyte>(-6, 5));

            Assert.Equal(-6, range.Min);
        }

        [Fact]
        public void Max_ReturnsMaxValue()
        {
            var range = new RangeCompound<sbyte>(new Range<sbyte>(0, 2), new Range<sbyte>(-6, 5));

            Assert.Equal(5, range.Max);
        }

        [Fact]
        public void InRange_ValueIsInAnyRange_ReturnsTrue()
        {
            var range = new RangeCompound<sbyte>(new Range<sbyte>(10, 20), new Range<sbyte>(-6, 5));

            Assert.True(range.InRange(0));
            Assert.True(range.InRange(15));
        }
    }

    public class RangeTests
    {
        [Fact]
        public void RangeConstructor_FirstBoundryIsLessThanSecondBoundry_MinValueIsLessThanOrEqualToMaxValue()
        {
           var range =  new Range<byte>(1, 3);

            Assert.True(range.Min <= range.Max);
        }

        [Fact]
        public void RangeConstructor_FirstBoundryIsGreaterThanSecondBoundry_MinValueIsLessThanOrEqualToMaxValue()
        {
            var range = new Range<byte>(5, 3);

            Assert.True(range.Min <= range.Max);
        }

        [Theory]
        [InlineData(0, 10d, 5)]
        [InlineData(0,double.PositiveInfinity, double.MaxValue)]
        public void InRange_ValueIsInRange_ReturnsTrue(double b1, double b2, double value)
        {
            var range = new Range<double>(b1, b2);

            Assert.True(range.InRange(value));
        }

        [Theory]
        [InlineData(0, 10, 20)]
        [InlineData(0, double.PositiveInfinity, double.MinValue)]
        public void InRange_ValueIsNotInRange_ReturnsFalse(double b1, double b2, double value)
        {
            var range = new Range<double>(b1, b2);

            Assert.False(range.InRange(value));
        }

        [Theory]
        [InlineData(0 , 10, 10, RangeBoundaries.InIn)]
        [InlineData(0, 10, 0, RangeBoundaries.InIn)]
        [InlineData(0, 10, 0, RangeBoundaries.InOut)]
        [InlineData(0, 10, 10, RangeBoundaries.OutIn)]
        public void InRange_ValueIsOnBoundry_ReturnsTrue(byte min, byte max, byte value, RangeBoundaries boundaries)
        {
            var range = new Range<byte>(min, max, boundaries);

            Assert.True(range.InRange(value));
        }

        [Theory]
        [InlineData(0, 10, 10, RangeBoundaries.OutOut)]
        [InlineData(0, 10, 0, RangeBoundaries.OutOut)]
        [InlineData(0, 10, 0, RangeBoundaries.OutIn)]
        [InlineData(0, 10, 10, RangeBoundaries.InOut)]
        public void InRange_ValueIsOnBoundry_ReturnsFalse(byte min, byte max, byte value, RangeBoundaries boundaries)
        {
            var range = new Range<byte>(min, max, boundaries);

            Assert.False(range.InRange(value));
        }
    }
}