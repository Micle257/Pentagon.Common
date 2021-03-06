﻿// -----------------------------------------------------------------------
//  <copyright file="Enumeration.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon
{
    using System;
    using System.Collections.Generic;
    using Collections;
    using JetBrains.Annotations;

    /// <summary> Represents a custom implementation of <see cref="Enum" />. </summary>
    public class Enumeration : IEnumeration, IValueDataType<Enumeration>
    {
        /// <summary> The unspecified name. </summary>
        const string UnspecifiedName = "Unspecified";

        /// <summary> The instance container. </summary>
        [NotNull]
        public static readonly TypeInstanceContainer<Enumeration> InstanceContainer = new TypeInstanceContainer<Enumeration>();

        /// <summary> Initializes a new instance of the <see cref="Enumeration" /> class. </summary>
        public Enumeration() : this(0, UnspecifiedName) { }

        /// <summary> Initializes a new instance of the <see cref="Enumeration" /> class. </summary>
        /// <param name="index"> The index. </param>
        /// <param name="name"> The name. </param>
        public Enumeration(int index, string name)
        {
            Index = index;
            Name = name;
            HasValue = true;
            InstanceContainer.Add(this);
        }

        /// <inheritdoc />
        public int Index { get; }

        /// <inheritdoc />
        public string Name { get; }

        /// <inheritdoc />
        public bool HasValue { get; }

        #region Operators

        /// <inheritdoc />
        public static bool operator <(Enumeration left, Enumeration right) => Comparer<Enumeration>.Default.Compare(left, right) < 0;

        /// <inheritdoc />
        public static bool operator >(Enumeration left, Enumeration right) => Comparer<Enumeration>.Default.Compare(left, right) > 0;

        /// <inheritdoc />
        public static bool operator <=(Enumeration left, Enumeration right) => Comparer<Enumeration>.Default.Compare(left, right) <= 0;

        /// <inheritdoc />
        public static bool operator >=(Enumeration left, Enumeration right) => Comparer<Enumeration>.Default.Compare(left, right) >= 0;

        /// <inheritdoc />
        public static bool operator ==(Enumeration left, Enumeration right) => Equals(left, right);

        /// <inheritdoc />
        public static bool operator !=(Enumeration left, Enumeration right) => !Equals(left, right);

        #endregion

        #region IEquatable members

        /// <inheritdoc />
        public bool Equals([CanBeNull] Enumeration other)
        {
            if (ReferenceEquals(null, other))
                return false;
            if (ReferenceEquals(this, other))
                return true;
            return Index == other.Index;
        }

        /// <inheritdoc />
        public override bool Equals([CanBeNull] object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;
            if (ReferenceEquals(this, obj))
                return true;
            if (obj.GetType() != GetType())
                return false;
            return Equals((Enumeration) obj);
        }

        /// <inheritdoc />
        public override int GetHashCode() => Index;

        #endregion

        /// <inheritdoc />
        public int CompareTo([CanBeNull] Enumeration other)
        {
            if (ReferenceEquals(this, other))
                return 0;
            if (ReferenceEquals(null, other))
                return 1;
            return Index.CompareTo(other.Index);
        }

        /// <inheritdoc />
        public int CompareTo([CanBeNull] object obj)
        {
            if (ReferenceEquals(null, obj))
                return 1;
            if (ReferenceEquals(this, obj))
                return 0;
            if (!(obj is Enumeration))
                throw new ArgumentException($"Object must be of type {nameof(Enumeration)}");
            return CompareTo((Enumeration) obj);
        }

        /// <inheritdoc />
        public override string ToString() => Name;
    }
}