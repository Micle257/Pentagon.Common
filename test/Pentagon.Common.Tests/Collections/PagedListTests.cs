namespace Pentagon.Common.Tests.Collections {
    using System.Collections.Generic;
    using Pentagon.Collections;
    using Xunit;

    public class PagedListTests
    {
        [Theory]
        [MemberData(nameof(PagedListTestSet.Valid), MemberType = typeof(PagedListTestSet))]
        public void PageSize_ReturnsCorrectValue(PagedListTestSet set)
        {
            // ARRANGE
            var list = new PagedList<int>(set.Input.Source, set.Input.TotalCount, set.Input.PageSize, set.Input.PageIndex);

            // ASSERT
            Assert.Equal(set.Expected.PageSize,list.PageSize);
        }

        [Theory]
        [MemberData(nameof(PagedListTestSet.Valid), MemberType = typeof(PagedListTestSet))]
        public void PageIndex_ReturnsCorrectValue(PagedListTestSet set)
        {
            // ARRANGE
            var list = new PagedList<int>(set.Input.Source, set.Input.TotalCount, set.Input.PageSize, set.Input.PageIndex);

            // ASSERT
            Assert.Equal(set.Expected.PageIndex, list.PageIndex);
        }

        [Theory]
        [MemberData(nameof(PagedListTestSet.Valid), MemberType = typeof(PagedListTestSet))]
        public void Count_ReturnsCorrectValue(PagedListTestSet set)
        {
            // ARRANGE
            var list = new PagedList<int>(set.Input.Source, set.Input.TotalCount, set.Input.PageSize, set.Input.PageIndex);

            // ASSERT
            Assert.Equal(set.Expected.Count, list.Count);
        }

        [Theory]
        [MemberData(nameof(PagedListTestSet.Valid), MemberType = typeof(PagedListTestSet))]
        public void PageNumber_ReturnsCorrectValue(PagedListTestSet set)
        {
            // ARRANGE
            var list = new PagedList<int>(set.Input.Source, set.Input.TotalCount, set.Input.PageSize, set.Input.PageIndex);

            // ASSERT
            Assert.Equal(set.Expected.PageNumber, list.PageNumber);
        }

        [Theory]
        [MemberData(nameof(PagedListTestSet.Valid), MemberType = typeof(PagedListTestSet))]
        public void TotalCount_ReturnsCorrectValue(PagedListTestSet set)
        {
            // ARRANGE
            var list = new PagedList<int>(set.Input.Source, set.Input.TotalCount, set.Input.PageSize, set.Input.PageIndex);

            // ASSERT
            Assert.Equal(set.Expected.TotalCount, list.TotalCount);
        }

        [Theory]
        [MemberData(nameof(PagedListTestSet.Valid), MemberType = typeof(PagedListTestSet))]
        public void TotalPages_ReturnsCorrectValue(PagedListTestSet set)
        {
            // ARRANGE
            var list = new PagedList<int>(set.Input.Source, set.Input.TotalCount, set.Input.PageSize, set.Input.PageIndex);

            // ASSERT
            Assert.Equal(set.Expected.TotalPages, list.TotalPages);
        }

        [Theory]
        [MemberData(nameof(PagedListTestSet.Valid), MemberType = typeof(PagedListTestSet))]
        public void HasNextPage_ReturnsCorrectValue(PagedListTestSet set)
        {
            // ARRANGE
            var list = new PagedList<int>(set.Input.Source, set.Input.TotalCount, set.Input.PageSize, set.Input.PageIndex);

            // ASSERT
            Assert.Equal(set.Expected.HasNextPage, list.HasNextPage);
        }

        [Theory]
        [MemberData(nameof(PagedListTestSet.Valid), MemberType = typeof(PagedListTestSet))]
        public void HasPreviousPage_ReturnsCorrectValue(PagedListTestSet set)
        {
            // ARRANGE
            var list = new PagedList<int>(set.Input.Source, set.Input.TotalCount, set.Input.PageSize, set.Input.PageIndex);

            // ASSERT
            Assert.Equal(set.Expected.HasPreviousPage, list.HasPreviousPage);
        }

        public class PagedListTestSet
        {
            public InputData Input { get; } = new InputData();

            public ExpectedData Expected { get; } = new ExpectedData();

            public static TheoryData<PagedListTestSet> Valid()
            {
                var data = new TheoryData<PagedListTestSet>
                           {
                                   new PagedListTestSet
                                   {
                                           Input =
                                           {
                                                   Source = new[] {1, 9, 0, 5, -4},
                                                   TotalCount = 6,
                                                   PageSize = 5,
                                                   PageIndex = 0
                                           },
                                           Expected =
                                           {
                                                   PageSize = 5,
                                                   TotalCount = 6,
                                                   Count = 5,
                                                   PageIndex = 0,
                                                   PageNumber =  1,
                                                   TotalPages = 2,
                                                   HasNextPage = true,
                                                   HasPreviousPage = false
                                           }
                                   },
                                   new PagedListTestSet
                                   {
                                           Input =
                                           {
                                                   Source     = new[] {1, 9, 0, 5, -4,9},
                                                   TotalCount = 6,
                                                   PageSize   = 6,
                                                   PageIndex  = 0
                                           },
                                           Expected =
                                           {
                                                   PageSize        = 6,
                                                   TotalCount      = 6,
                                                   Count           = 6,
                                                   PageIndex       = 0,
                                                   PageNumber      =  1,
                                                   TotalPages      = 1,
                                                   HasNextPage     = false,
                                                   HasPreviousPage = false
                                           }
                                   },
                                   new PagedListTestSet
                                   {
                                           Input =
                                           {
                                                   Source     = new[] {15,78,98},
                                                   TotalCount = 15487,
                                                   PageSize   = 3,
                                                   PageIndex  = 10
                                           },
                                           Expected =
                                           {
                                                   PageSize        =3,
                                                   TotalCount      = 15487,
                                                   Count           = 3,
                                                   PageIndex       = 10,
                                                   PageNumber      =  11,
                                                   TotalPages      = 5163,
                                                   HasNextPage     = true,
                                                   HasPreviousPage = true
                                           }
                                   }
                           };


                return data;
            }

            public class ExpectedData
            {
                public int PageSize { get; set; }

                public int PageIndex { get; set; }

                public int PageNumber { get; set; }

                public int TotalCount { get; set; }

                public int TotalPages { get; set; }

                public bool HasPreviousPage { get; set; }

                public bool HasNextPage { get; set; }

                public int Count { get; set; }
            }

            public class InputData
            {
                public IEnumerable<int> Source { get; set; }

                public int? TotalCount { get; set; }

                public int? PageSize { get; set; }

                public int? PageIndex { get; set; }
            }
        }
    }
}