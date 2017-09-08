// -----------------------------------------------------------------------
//  <copyright file="TextDateFormat.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;

    /// <summary> Represents a pair of <see cref="string" /> value and <see cref="DateTime" /> value. </summary>
    public class TextDateFormat : IComparable<TextDateFormat>, IComparable, IEquatable<TextDateFormat>
    {
        /// <summary> Initializes a new instance of the <see cref="TextDateFormat" /> class. </summary>
        /// <param name="txt"> The text. </param>
        /// <param name="date"> The date. </param>
        public TextDateFormat(string txt, DateTime date)
        {
            Text = txt;
            Date = date;
            HasDate = true;
        }

        /// <summary> Gets the text. </summary>
        /// <value> The string value of text. </value>
        public string Text { get; }

        /// <summary> Gets the date. </summary>
        /// <value> The DateTime value of date. </value>
        public DateTime Date { get; }

        /// <summary> Gets or sets a value indicating whether this instance has date. </summary>
        /// <value>
        ///     <c> true </c> if this instance has date; otherwise, <c> false </c>. </value>
        public bool HasDate { get;  }

        /// <inheritdoc />
        public static bool operator ==(TextDateFormat left, TextDateFormat right) => Equals(left, right);

        /// <inheritdoc />
        public static bool operator !=(TextDateFormat left, TextDateFormat right) => !Equals(left, right);

        /// <inheritdoc />
        public static bool operator <(TextDateFormat left, TextDateFormat right) => Comparer<TextDateFormat>.Default?.Compare(left, right) < 0;

        /// <inheritdoc />
        public static bool operator >(TextDateFormat left, TextDateFormat right) => Comparer<TextDateFormat>.Default?.Compare(left, right) > 0;

        /// <inheritdoc />
        public static bool operator <=(TextDateFormat left, TextDateFormat right) => Comparer<TextDateFormat>.Default?.Compare(left, right) <= 0;

        /// <inheritdoc />
        public static bool operator >=(TextDateFormat left, TextDateFormat right) => Comparer<TextDateFormat>.Default?.Compare(left, right) >= 0;

        /// <inheritdoc />
        int IComparable.CompareTo(object obj)
        {
            if (ReferenceEquals(null, obj))
                return 1;
            if (ReferenceEquals(this, obj))
                return 0;
            if (!(obj is TextDateFormat))
                throw new ArgumentException($"Object must be of type {nameof(TextDateFormat)}");
            return CompareTo((TextDateFormat) obj);
        }

        /// <inheritdoc />
        public int CompareTo(TextDateFormat other)
        {
            if (ReferenceEquals(this, other))
                return 0;
            if (ReferenceEquals(null, other))
                return 1;
            var textComparison = string.Compare(Text, other.Text, StringComparison.Ordinal);
            if (textComparison != 0)
                return textComparison;
            return Date.CompareTo(other.Date);
        }

        /// <inheritdoc />
        public bool Equals(TextDateFormat other)
        {
            if (ReferenceEquals(null, other))
                return false;
            if (ReferenceEquals(this, other))
                return true;
            return string.Equals(Text, other.Text) && Date.Equals(other.Date);
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;
            if (ReferenceEquals(this, obj))
                return true;
            if (obj.GetType() != GetType())
                return false;
            return Equals((TextDateFormat) obj);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            unchecked
            {
                return ((Text?.GetHashCode() ?? 0) * 397) ^ Date.GetHashCode();
            }
        }

        /// <inheritdoc />
        public override string ToString() => $"{Text}{(HasDate ? Date.ToString(CultureInfo.InvariantCulture /*Time.DayFormat*/) : "")}";
    }
}