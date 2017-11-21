// -----------------------------------------------------------------------
//  <copyright file="DateTimeExtensions.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Extensions
{
    using System;
    using System.Globalization;

    /// <summary> Contains extension methods for <see cref="DateTime" />. </summary>
    public static class DateTimeExtensions
    {
        /// <summary> Gets the day difference between this time and current date. </summary>
        /// <param name="dateTime"> The date time. </param>
        /// <returns> A <see cref="TimeSpan" />. </returns>
        public static TimeSpan GetDateDifference(this DateTime dateTime) => dateTime.Date - DateTime.Now.Date;

        /// <summary> Gets the week number of this <see cref="DateTime" />. </summary>
        /// <param name="dateTime"> The date time. </param>
        /// <param name="weekRule"> The week rule. </param>
        /// <param name="dayOfWeek"> The day of week. </param>
        /// <returns> A <see cref="int" /> of week number, -or- negative 1 if failed to find the number. </returns>
        public static int GetWeekNumber(this DateTime dateTime, CalendarWeekRule weekRule = CalendarWeekRule.FirstDay, DayOfWeek dayOfWeek = DayOfWeek.Monday)
            => CultureInfo.CurrentCulture?.Calendar?.GetWeekOfYear(dateTime, weekRule, dayOfWeek) ?? -1;
    }
}