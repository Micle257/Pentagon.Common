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
        /// <summary> Gets the day difference between this time and current date.. </summary>
        /// <param name="dateTime"> The date time. </param>
        /// <returns> A <see cref="TimeSpan" />. </returns>
        public static TimeSpan GetDateSpan(this DateTime dateTime) => dateTime.Date - DateTime.Now.Date;

        /// <summary> Gets the week of year number.. </summary>
        /// <param name="dateTime"> The date time. </param>
        /// <returns> A <see cref="int" />. </returns>
        public static int GetWeekNumber(this DateTime dateTime)
            => CultureInfo.CurrentCulture?.Calendar?.GetWeekOfYear(dateTime, CalendarWeekRule.FirstDay, DayOfWeek.Monday) ?? -1;
    }
}