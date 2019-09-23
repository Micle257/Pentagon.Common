// -----------------------------------------------------------------------
//  <copyright file="HierarchyListTests.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Common.Tests
{
    using Pentagon.Collections;
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
        public void Constructor_CreatesRootWithCorrectSiblings()
        {
            var property = new HierarchyList<int>(5);

            Assert.NotNull(property.Root.Siblings);
            Assert.Equal(0, property.Root.Siblings.Count);
        }
    }
}