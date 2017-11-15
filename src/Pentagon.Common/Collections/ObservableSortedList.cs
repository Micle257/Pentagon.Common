// -----------------------------------------------------------------------
//  <copyright file="ObservableSortedList.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Collections
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.ComponentModel;
    using System.Linq;
    using Extensions;
    using Helpers;
    using JetBrains.Annotations;

    /// <summary> Represents an observable collection which maintains its items in sorted order. It also supports filtering its items. The items are sorted when they change. </summary>
    /// <typeparam name="T"> The type of an item. </typeparam>
    public sealed class ObservableSortedList<T> : ObservableObject, IObservableReadOnlyList<T>, IObservableCollection<T>, IList<T>, IFilterableCollection<T>
        where T : INotifyPropertyChanged
    {
        /// <summary> Inner collection representing this instance in outer scope (filtered items). </summary>
        [NotNull]
        readonly List<T> _items;

        /// <summary> The collection of all items. </summary>
        [NotNull]
        readonly List<T> _allItems;

        /// <summary> The comparer of this list. </summary>
        [NotNull]
        IComparer<T> _comparer;

        /// <summary> Initializes a new instance of the <see cref="ObservableSortedList{T}" /> class. </summary>
        /// <param name="comparer"> The comparer. </param>
        /// <param name="filter"> The filter. </param>
        public ObservableSortedList(IComparer<T> comparer = null, ICollectionFilter<T> filter = null)
        {
            _allItems = new List<T>();
            _items = new List<T>();
            _comparer = comparer ?? (Comparer<T>.Default ?? throw new ArgumentNullException());
            Filter = filter ?? new CollectionFilter<T>();
        }

        /// <summary> Initializes a new instance of the <see cref="ObservableSortedList{T}" /> class. </summary>
        /// <param name="items"> The items. </param>
        /// <param name="comparer"> The comparer. </param>
        /// <param name="filter"> The filter. </param>
        public ObservableSortedList([NotNull] IEnumerable<T> items, IComparer<T> comparer = null, ICollectionFilter<T> filter = null)
            : this(comparer, filter)
        {
            Require.NotNull(() => items);

            foreach (var item in items)
                Add(item);
        }

        /// <inheritdoc />
        public event NotifyCollectionChangedEventHandler CollectionChanged;

        /// <summary> Occurs when a filter of this collection has changed. </summary>
        public event EventHandler<ICollectionFilter<T>> FilterChanged;

        /// <summary> Occurs when a comparer of this collection has changed. </summary>
        public event EventHandler<IComparer<T>> ComparerChanged;

        /// <inheritdoc />
        public bool IsReadOnly => false;

        /// <inheritdoc />
        public ICollectionFilter<T> Filter { get; private set; }

        /// <inheritdoc />
        public int Count => _items.Count;

        /// <inheritdoc />
        public T this[int index]
        {
            get => _items[index];
            set => throw new InvalidOperationException(message: "Cannot set an item at an arbitrary index in a ObservableSortedList.");
        }

        /// <inheritdoc />
        public IEnumerator<T> GetEnumerator() => _items.GetEnumerator();

        /// <inheritdoc />
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        /// <inheritdoc />
        public void Add(T item)
        {
            Require.NotDefault(() => item);
            //var i = _allItems.BinarySearch(item, _comparer);
            //if (i < 0)
            //    i = ~i;
            //else
            //{
            //    do
            //    {
            //        i++;
            //    } while (i < Count && _comparer.Compare(_allItems[i], item) == 0);
            //}
            var i = _allItems.FindOrderIndex(v => _comparer.Compare(_allItems[v], item) < 0);

            _allItems.Insert(i, item);

            if (Filter.Predicate(item))
            {
                var inf = _items.FindOrderIndex(v => _comparer.Compare(_items[v], item) < 0);
                _items.Insert(inf, item);
                OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, item, i));
                OnPropertyChanged(nameof(Count));
            }

            item.PropertyChanged += OnItemPropertyChanged;
        }

        /// <inheritdoc />
        public void Clear()
        {
            foreach (var item in _allItems)
                item.PropertyChanged -= OnItemPropertyChanged;
            _allItems.Clear();
            _items.Clear();
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
            OnPropertyChanged(nameof(Count));
        }

        /// <inheritdoc />
        public bool Contains(T item) => _items.Contains(item);

        /// <inheritdoc />
        public void CopyTo(T[] array, int arrayIndex) => _allItems.CopyTo(array, arrayIndex);

        /// <inheritdoc />
        public bool Remove(T item)
        {
            var i = _allItems.IndexOf(item);
            if (i < 0)
                return false;

            // tries to removed from filter list
            var inf = _items.IndexOf(item);
            if (inf >= 0)
            {
                _items.RemoveAt(inf);
                OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, item, i));
                OnPropertyChanged(nameof(Count));
            }

            item.PropertyChanged -= OnItemPropertyChanged;
            _allItems.RemoveAt(i);
            return true;
        }

        /// <inheritdoc />
        public int IndexOf(T item) => _items.IndexOf(item);

        /// <inheritdoc />
        void IList<T>.Insert(int index, T item)
        {
            throw new InvalidOperationException(message: "Cannot insert an item at an arbitrary index into a ObservableSortedList.");
        }

        /// <inheritdoc />
        public void RemoveAt(int index)
        {
            var item = _items[index];
            item.PropertyChanged -= OnItemPropertyChanged;
            _items.RemoveAt(index);
            _allItems.Remove(item);
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, item, index));
            OnPropertyChanged(nameof(Count));
        }

        /// <inheritdoc />
        public void ApplyFilter(ICollectionFilter<T> filter)
        {
            Filter = filter;
            FilterChanged?.Invoke(this, filter);
            FilterItems();
        }

        /// <summary> Changes the comparer of the sorted list and sorts it. </summary>
        /// <param name="comparer"> The new comparer. </param>
        public void ApplyComparer(IComparer<T> comparer)
        {
            if (comparer.Equals(_comparer))
                return;
            _comparer = comparer;
            ComparerChanged?.Invoke(this, _comparer);
            Sort();
        }

        /// <summary> Fires the <see cref="CollectionChanged" /> event. </summary>
        /// <param name="args"> The event arguments. </param>
        void OnCollectionChanged(NotifyCollectionChangedEventArgs args)
        {
            CollectionChanged?.Invoke(this, args);
        }

        /// <summary> Determines the position of the item and puts it in sorted order. </summary>
        /// <param name="item"> The item. </param>
        /// <param name="list"> The list where to order. </param>
        void OrderItem(T item, [NotNull] List<T> list)
        {
            var oldIndex = list.IndexOf(item);

            if (Count <= 1
                || (oldIndex == 0 || _comparer.Compare(list[oldIndex - 1], item) <= 0)
                && (oldIndex == Count - 1 || _comparer.Compare(item, list[oldIndex + 1]) <= 0))
                return;

            list.RemoveAt(oldIndex);
            var newIndex = list.BinarySearch(item, _comparer);
            if (newIndex < 0)
                newIndex = ~newIndex;
            else
            {
                do
                {
                    newIndex++;
                } while (newIndex < list.Count && _comparer.Compare(list[newIndex], item) == 0);
            }

            list.Insert(newIndex, item);

            if (ReferenceEquals(list, _items))
                OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Move, new[] {item}, newIndex, oldIndex));
        }

        /// <summary> Sortes the collection in sorted order. </summary>
        void Sort()
        {
            foreach (var item in _allItems.ToList())
                OrderItem(item, _allItems);
            foreach (var item in _items.ToList())
                OrderItem(item, _items);
        }

        /// <summary> Filters every element in the collection. </summary>
        void FilterItems()
        {
            var filterList = _allItems.Where(s => Filter.Predicate(s)).ToList();

            if (filterList.Count == _allItems.Count)
                return;

            var intersect = _items.Intersect(filterList).ToList();
            var toRemove = _items.Except(intersect).ToList();
            var toAdd = filterList.Except(intersect).ToList();

            foreach (var item in toRemove)
            {
                _items.Remove(item);
                OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, item));
            }

            foreach (var item in toAdd)
            {
                var i = _items.FindOrderIndex(v => _comparer.Compare(_items[v], item) < 0);
                _items.Insert(i, item);
                OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, item, i));
            }
        }

        /// <summary> Fires the <see cref="INotifyPropertyChanged.PropertyChanged" /> event on the item. </summary>
        /// <param name="sender"> The item. </param>
        /// <param name="e"> The event arguments. </param>
        void OnItemPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            var item = (T) sender;
            FilterItems();
            OrderItem(item, _allItems);
            OrderItem(item, _items);
        }
    }
}