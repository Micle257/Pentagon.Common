// -----------------------------------------------------------------------
//  <copyright file="RangeCompoundTests.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

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
            var range = new RangeCompound<sbyte>(new Range<sbyte>(0, 2), new Range<sbyte>(-6, 5));

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
}