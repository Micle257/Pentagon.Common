// -----------------------------------------------------------------------
//  <copyright file="TypeInstanceContainer.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Helpers
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using JetBrains.Annotations;

    /// <summary> Represents an inner collection of static instances created during type's lifetime. </summary>
    /// <typeparam name="T"> The type of instance. </typeparam>
    public class TypeInstanceContainer<T> : IReadOnlyList<T>
        where T : IComparable<T>
    {
        /// <summary> The instantiated elements. </summary>
        [NotNull]
        readonly IList<T> _items = new List<T>();

        /// <summary> Initializes a new instance of the <see cref="TypeInstanceContainer{T}" /> class. </summary>
        public TypeInstanceContainer() { }

        /// <summary> Initializes a new instance of the <see cref="TypeInstanceContainer{T}" /> class. </summary>
        /// <param name="type"> The type of the container helper class from where the instances are obtained. </param>
        public TypeInstanceContainer(Type type)
        {
            RuntimeHelpers.RunClassConstructor(type.TypeHandle);
        }

        /// <inheritdoc />
        public int Count => _items.Count;

        /// <inheritdoc />
        public T this[int index] => _items[index];

        /// <inheritdoc />
        public IEnumerator<T> GetEnumerator() => _items.GetEnumerator();

        /// <inheritdoc />
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        /// <summary> Adds the specified item. </summary>
        /// <param name="item"> The item. </param>
        public void Add(T item)
        {
            if (_items.Any(i => i.CompareTo(item) == 0))
                return;
            // throw new ArgumentException("Element is already in collection");

            _items.Add(item);
        }
    }
}