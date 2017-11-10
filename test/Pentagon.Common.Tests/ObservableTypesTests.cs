// -----------------------------------------------------------------------
//  <copyright file="ObservableTypesTests.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Common.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using Helpers;
    using Moq;
    using Xunit;

    public class ObservableTypesTests
    {
        public class ObservableObjectTests
        {
            [Fact]
            public void PropertyChanged_ValueIsChanged_Fires()
            {
                var o = new Test<int>
                        {
                            Value = 5
                        };
                var eventFired = false;
                o.PropertyChanged += (sender, args) => eventFired = true;

                o.Value = 10;

                Assert.True(eventFired);
            }

            [Fact]
            public void PropertyChanged_ValueIsNotChanged_DoesNotFire()
            {
                var o = new Test<int>
                        {
                            Value = 5
                        };
                var eventFired = false;
                o.PropertyChanged += (sender, args) => eventFired = true;

                o.Value = 5;

                Assert.True(!eventFired);
            }

            [Fact]
            public void PropertyChanged_EventIsFired_PropertyNameIsEqualToEventArgumentPropertyName()
            {
                var o = new Test<int>
                        {
                            Value = 5
                        };
                var name = default(string);
                o.PropertyChanged += (sender, args) => name = args.PropertyName;
                o.Value = 6;

                Assert.Equal(expected: "Value", actual: name);
            }

            public class Test<T> : ObservableObject
            {
                T _value;

                public T Value
                {
                    get => _value;
                    set
                    {
                        if (Equals(value, _value))
                            return;
                        _value = value;
                        OnPropertyChanged();
                    }
                }
            }
        }

        public class ObservableSortedListTests
        {
            public ObservableSortedListTests()
            {
                Integers = new ObservableSortedList<Integer>(Integer.ValueComparer);
            }

            ObservableSortedList<Integer> Integers { get; }

            [Fact]
            public void Add_ValuesAdded_SortsValues()
            {
                Integers.Add(new Integer(5));
                Integers.Add(new Integer(7));
                Integers.Add(new Integer(4));
                
                Assert.Equal(3, Integers.Count);
                Assert.Equal(4, Integers[0].Value);
                Assert.Equal(5, Integers[1].Value);
                Assert.Equal(7, Integers[2].Value);
            }

            [Fact]
            public void CollectionChanges_ItemIsAdded_Fires()
            {
                var eventFired = false;
                Integers.CollectionChanged += (sender, args) => { eventFired = args.Action == NotifyCollectionChangedAction.Add; };

                Integers.Add(new Integer(4));

                Assert.True(eventFired);
            }

            [Fact]
            public void ApplyComparer_ReordersValues()
            {
                Integers.Add(new Integer(5));
                Integers.Add(new Integer(7));
                Integers.Add(new Integer(4));

                Integers.ApplyComparer(Integer.ReverseComparer);

                Assert.Equal(7, Integers[0].Value);
                Assert.Equal(5, Integers[1].Value);
                Assert.Equal(4, Integers[2].Value);
            }

            [Fact]
            public void ShouldCollectionSortWhenValueIsChanged()
            {
                Integers.Add(new Integer(5));
                Integers.Add(new Integer(7));
                Integers.Add(new Integer(4));
                var i = new Integer(8);
                Integers.Add(i);

                i.Value = 0;

                Assert.Equal(0, Integers[0].Value);
            }

            [Fact]
            public void ShouldFilterItems()
            {
                var m = new Mock<ICollectionFilter<Integer>>();
                m.SetupGet(c => c.Predicate).Returns(i => i.Value < 10);

                var filter = m.Object;
                var coll = new ObservableSortedList<Integer>(Integer.ValueComparer, filter)
                           {
                               new Integer(3),
                               new Integer(5),
                               new Integer(1),
                               new Integer(15)
                           };

                Assert.Equal(3, coll.Count);
                Assert.True(!coll.Contains(new Integer(15)));
            }

            [Fact]
            public void ShouldRefilterWhenFilterConditionIsChanged()
            {
                var filter = new CollectionFilter<Integer>(i => i.Value > 5);
                var coll = new ObservableSortedList<Integer>(Integer.ValueComparer, filter)
                           {
                               new Integer(2),
                               new Integer(5),
                               new Integer(1),
                               new Integer(4),
                               new Integer(7),
                               new Integer(9)
                           };

                coll.ApplyFilter(new CollectionFilter<Integer>(i => i.Value < 3));

                Assert.Equal(2, coll.Count);
                Assert.True(coll.Contains(new Integer(1)));
                Assert.True(coll.Contains(new Integer(2)));
            }

           public class Integer : ObservableObject, IEquatable<Integer>
            {
                int _value;
                readonly int _initialValue;

                /// <inheritdoc />
                public Integer(int value)
                {
                    _initialValue = value;
                    _value = value;
                }

                public static Comparer<Integer> ReverseComparer { get; } = new ReverseRelationalComparer();

                public static Comparer<Integer> ValueComparer { get; } = new ValueRelationalComparer();

                public int Value
                {
                    get => _value;
                    set
                    {
                        if (value == _value)
                            return;
                        _value = value;
                        OnPropertyChanged();
                    }
                }
                
                #region IEquatable members

                /// <inheritdoc />
                public bool Equals(Integer other)
                {
                    if (ReferenceEquals(null, other))
                        return false;
                    if (ReferenceEquals(this, other))
                        return true;
                    return _value == other._value;
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
                    return Equals((Integer) obj);
                }

                /// <inheritdoc />
                public override int GetHashCode() => _initialValue;

                #endregion

                /// <inheritdoc />
                public override string ToString() => $"{nameof(Value)}: {Value}";

                sealed class ReverseRelationalComparer : Comparer<Integer>
                {
                    public override int Compare(Integer x, Integer y)
                    {
                        if (ReferenceEquals(x, y))
                            return 0;
                        if (ReferenceEquals(null, y))
                            return -1;
                        if (ReferenceEquals(null, x))
                            return 1;
                        return y._value.CompareTo(x._value);
                    }
                }

                sealed class ValueRelationalComparer : Comparer<Integer>
                {
                    public override int Compare(Integer x, Integer y)
                    {
                        if (ReferenceEquals(x, y))
                            return 0;
                        if (ReferenceEquals(null, y))
                            return 1;
                        if (ReferenceEquals(null, x))
                            return -1;
                        return x._value.CompareTo(y._value);
                    }
                }
            }
        }
    }
}