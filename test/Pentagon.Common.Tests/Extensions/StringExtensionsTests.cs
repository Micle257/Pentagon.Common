// -----------------------------------------------------------------------
//  <copyright file="StringExtensionsTests.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Common.Tests.Extensions
{
    using System;
    using Pentagon.Extensions;
    using Xunit;

    public class StringExtensionsTests
    {
        [Fact]
        public void Quote_ReturnsCorrectValue()
        {
            var text = "hi ";

            var quote = text.Quote();

            Assert.Equal("'hi '", quote);
        }

        [Theory]
        [InlineData("3 ", 3)]
        [InlineData("asd v", null)]
        public void ToInt_ReturnsCorrectValue(string input, int? expected)
        { 
            var integer = input.ToInt();

            Assert.Equal(expected, integer);
        }

        [Fact]
        public void SplitToLines_ReturnsCorrectIteration()
        {
            var text = "first\nsecond\rthird\nfourth\r\nlast";

            var lines = text.SplitToLines();

            Assert.Equal(new[] {"first", "second", "third", "fourth", "last"}, lines);
        }

        [Fact]
        public void SplitToLines_InputIsNull_ReturnsEmptyIteration()
        {
            var lines = StringExtensions.SplitToLines(null);

            Assert.Equal(Array.Empty<string>(), lines);
        }
    }
}