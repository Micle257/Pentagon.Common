﻿// -----------------------------------------------------------------------
//  <copyright file="ContainerContainmentTests.cs" company="The Pentagon">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Utilities.Tests
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using Xunit;

    public class ContainerContainmentTests
    {
        [Fact]
        public void ShouldContainerAddItem()
        {
            IContainer<IItem> c = new TopContainer();

            c.AddItem(new Item(5));

            Assert.Equal(1, c.Count);
        }

        [Fact]
        public void ShouldValueIndexerReturnNegativeOneWhenNoValueIsFound()
        {
            var cont = new TopContainer();
            IItem item = new Item(3);
            Assert.Equal(-1, cont[item]);
        }

        [Fact]
        public void ShouldIndexerThrowExceptionWhenIndexIsOutOfRange()
        {
            IContainer<IItem> c = new TopContainer();

            Assert.Throws<ArgumentOutOfRangeException>(() => c[5]);
        }

        [Fact]
        public void ShouldContainerContainTheItem()
        {
            IContainer<IItem> c = new TopContainer();
            IItem item = new Item(5);

            c.AddItem(item);

            Assert.Same(item, c[0]);
        }

        [Fact]
        public void ShouldItemIndexerReturnSameObject()
        {
            IContainer<IItem> c = new TopContainer();
            IItem item = new Item(5);

            c.AddItem(item);

            Assert.Equal(0, c[item]);
        }
        
        class TopContainer : IContainer<IItem>
        {
            readonly List<IItem> _objects = new List<IItem>();
            
            public int Count => _objects.Count;
            
            public IItem this[int index] => _objects[index];
            
            public int this[IItem value] => _objects.IndexOf(value);

            public void AddItem(IItem item)
            {
                _objects.Add(item);
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
            
            public IEnumerator<IItem> GetEnumerator() => _objects.GetEnumerator();
        }

        interface IItem
        {
            int Value { get; }
        }

        class Item : IItem
        {
            public Item(int value)
            {
                Value = value;
            }
            public int Value { get; set; }
        }
    }
}