// -----------------------------------------------------------------------
//  <copyright file="CalendarHelper.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Helpers
{
    using System;
    using System.Globalization;

    public static class CalendarHelper
    {
        /// <summary> Gets the first date of week in the specified year. Uses ISO8601 standart. </summary>
        /// <param name="year"> The year. </param>
        /// <param name="weekOfYear"> The week number. </param>
        /// <returns> A <see cref="DateTime" /> of the first weekday. </returns>
        public static DateTime GetFirstDateOfWeek(int year, int weekOfYear)
        {
            var jan1 = new DateTime(year, 1, 1);
            var daysOffset = DayOfWeek.Thursday - jan1.DayOfWeek;

            var firstThursday = jan1.AddDays(daysOffset);
            var cal = CultureInfo.CurrentCulture.Calendar;
            var firstWeek = cal.GetWeekOfYear(firstThursday, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);

            if (firstWeek == 1)
                firstWeek = 0;

            var result = firstThursday.AddDays(firstWeek * 7);

            return result.AddDays(-3);
        }
    }
}