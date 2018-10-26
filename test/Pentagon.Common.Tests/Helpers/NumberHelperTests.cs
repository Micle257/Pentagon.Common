// -----------------------------------------------------------------------
//  <copyright file="NumberHelperTests.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Common.Tests.Helpers
{
    using Pentagon.Helpers;
    using Xunit;

    public class NumberHelperTests
    {
        [Theory]
        [InlineData(5, 2, 20)]
        [InlineData(5, 1, 10)]
        [InlineData(int.MaxValue, 2, -3)]
        [InlineData(int.MinValue, 1, 1)]
        [InlineData(0, 15, 0)]
        public void ShiftAndWrap_ReturnsCorrectValue(int value, int position, int expected)
        {
            var result = NumberHelper.ShiftAndWrap(value, position);

            Assert.Equal(expected, result);
        }
    }
}