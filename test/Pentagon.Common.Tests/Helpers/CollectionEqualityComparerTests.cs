// -----------------------------------------------------------------------
//  <copyright file="CollectionEqualityComparerTests.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Common.Tests.Helpers
{
    using Pentagon.Helpers;
    using Xunit;

    public class CollectionEqualityComparerTests
    {
        [Theory]
        [InlineData(new[] { 1, 2, 3 }, new[] { 1, 2, 3 }, true, true)]
        [InlineData(new[] { 1, 2, 3 }, new[] { 3, 1, 2 }, true, true)]
        [InlineData(new[] { 5, 6, 7 }, new[] { 3, 1, 2 }, true, false)]
        [InlineData(new[] { 1, 2, 3 }, new[] { 1, 2, 3, 2, 1 }, true, false)]
        [InlineData(new[] { 1, 2, 3 }, new[] { 5, 10, 7, 9, 4 }, true, false)]
        [InlineData(new[] { 1, 2, 3 }, new[] { 1, 2, 3 }, false, true)]
        [InlineData(new[] { 1, 2, 3 }, new[] { 3, 1, 2 }, false, false)]
        [InlineData(new[] { 5, 6, 7 }, new[] { 3, 1, 2 }, false, false)]
        [InlineData(new[] { 1, 2, 3 }, new[] { 1, 2, 3, 2, 1 }, false, false)]
        [InlineData(new[] { 1, 2, 3 }, new[] { 5, 10, 7, 9, 4 }, false, false)]
        public void Equals_ReturnCorrectResult(int[] collection1, int[] collection2, bool ignoreOrder, bool expected)
        {
            var comparer = new CollectionEqualityComparer<int>(ignoreOrder);

            var result = comparer.Equals(collection1, collection2);

            Assert.Equal(expected, result);
        }
    }
}