// -----------------------------------------------------------------------
//  <copyright file="HierarchyListTests.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Common.Tests.Collection.Tree
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Pentagon.Collections;
    using Pentagon.Collections.Tree;
    using Xunit;

    public class HierarchyListTests
    {
        [Fact]
        public void Constructor_CreatesDefinedRootProperty()
        {
            var property = new HierarchyList<int>(5);

            Assert.NotNull(property.Root);
        }

        [Fact]
        public void Constructor_CreatesRootWithCorrectValue()
        {
            var property = new HierarchyList<int>(5);

            Assert.Equal(5, property.Root.Value);
        }

        [Fact]
        public void Constructor_CreatesRootWithCorrectParent()
        {
            var property = new HierarchyList<int>(5);

            Assert.Null(property.Root.Parent);
        }

        [Fact]
        public void Constructor_CreatesRootWithCorrectDepth()
        {
            var property = new HierarchyList<int>(5);

            Assert.Equal(0, property.Root.Depth);
        }

        [Fact]
        public void Constructor_CreatesRootWithCorrectChildren()
        {
            var property = new HierarchyList<int>(5);

            Assert.NotNull(property.Root.Children);
            Assert.Equal(0, property.Root.Children.Count);
        }

        [Fact]
        public void FromDictionary_MultipleParents_ExpectedBehavior()
        {
            var ids = new[]
                      {
                              0,
                              1,
                              2,
                              3,
                              4,
                              5,
                              6,
                              7,
                              8,
                              10,
                              11
                      };

            // ARRANGE
            var map = new Dictionary<int, List<int>>
                      {
                              [ids[0]] = new List<int> { ids[1] , ids[2] , ids[3] },
                              [ids[1]] = new List<int> { ids[4], ids[5] },
                              [ids[2]] = new List<int> { ids[6], ids[7] },
                              //[ids[3]] = new List<Identity> { ids[3] },
                              //[ids[4]] = new List<Identity> { ids[0] },
                              //[5] = new List<Identity> {  },
                              //[6] = new List<int> {  },
                              [ids[7]] = new List<int> { ids[8] },
                //[8] = new List<int> {  },
                [ids[9]] = new List<int> { ids[8] },
                [ids[10]] = new List<int> { ids[9] },
                      };

            // ACT
            var hierarchyList = HierarchyList<int>.FromDictionaryFreely(map);

            var str = string.Join($"{Environment.NewLine}{Environment.NewLine}", hierarchyList.Select(a => a.ToTreeString()));
            // ASSERT
        }
    }
}