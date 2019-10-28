// -----------------------------------------------------------------------
//  <copyright file="Percentage.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon
{
    using System;
    using Extensions;
    using JetBrains.Annotations;

    /// <summary> Represents a numeric percentage value wrapped around <see cref="double" />. </summary>
    [PublicAPI]
    public readonly struct Percentage : IEquatable<Percentage>, IComparable<Percentage>
    {
        Percentage(double doubleValue)
        {
            DecimalValue = doubleValue;
        }

        /// <summary> Gets the decimal value. </summary>
        public double DecimalValue { get; }

        /// <summary> Gets the value of percentage. </summary>
        public double IntegerValue => DecimalValue * 100;

        #region Operators

        public static implicit operator double(Percentage value) => value.DecimalValue;

        public static implicit operator float(Percentage value) => Convert.ToSingle(value: value.DecimalValue);

        public static implicit operator decimal(Percentage value) => Convert.ToDecimal(value: value.DecimalValue);

        public static implicit operator Percentage(double value) => new Percentage(doubleValue: value);

        public static implicit operator Percentage(float value) => new Percentage(doubleValue: value);

        public static implicit operator Percentage(decimal value) => new Percentage(Convert.ToDouble(value: value));

        public static bool operator ==(Percentage left, Percentage right) => left.Equals(other: right);

        public static bool operator !=(Percentage left, Percentage right) => !left.Equals(other: right);

        #endregion

        public int CompareTo(Percentage other) => DecimalValue.CompareTo(value: other.DecimalValue);

        public static Percentage FromDecimalValue(double value) => new Percentage(doubleValue: value);

        public static Percentage FromIntegerValue(double value) => new Percentage(value / 100);

        public override string ToString() => $"{DecimalValue.RoundSignificantFigures(3) * 100}%";

        #region IEquatable members

        public bool Equals(Percentage other) => DecimalValue.EqualTo(b: other.DecimalValue);

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, objB: obj))
                return false;
            return obj is Percentage percentage && Equals(percentage);
        }

        public override int GetHashCode() => DecimalValue.GetHashCode();

        #endregion
    }
}