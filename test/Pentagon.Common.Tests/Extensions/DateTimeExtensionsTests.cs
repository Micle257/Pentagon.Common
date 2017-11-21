// -----------------------------------------------------------------------
//  <copyright file="DateTimeExtensionsTests.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Common.Tests.Extensions
{
    using System;
    using System.Globalization;
    using Pentagon.Extensions;
    using Xunit;

    public class DateTimeExtensionsTests
    {
        [Fact]
        public void GetDateDifference_SpanMatchesDateDifference()
        {
            var date = DateTime.Today.AddDays(-2);

            var diff = date.GetDateDifference();

            Assert.Equal(-2, diff.Days);
        }

        [Fact]
        public void GetDateDifference_DifferenceIsTimeUndependent()
        {
            var date = DateTime.Now.AddDays(-1);

            var diff = date.GetDateDifference();

            Assert.Equal(0, diff.Milliseconds);
        }

        [Theory]
        [InlineData(2018, 1, 7, 1)]
        [InlineData(2018, 1, 8, 2)]
        public void GetWeekNumber_ReturnsCorrectWeekNumber(int year, int month, int day, int weekNumber)
        {
            var date = new DateTime(year, month, day);

            var week = date.GetWeekNumber(CalendarWeekRule.FirstDay, DayOfWeek.Monday);

            Assert.Equal(weekNumber, week);
        }
    }
}