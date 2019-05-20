// -----------------------------------------------------------------------
//  <copyright file="PaginationHelperTests.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Common.Tests.Helpers
{
    using System.Linq;
    using Pentagon.Helpers;
    using Xunit;

    public class PaginationHelperTests
    {
        [Fact]
        public void CreatePagedList_ReturnsCorrectPageItems()
        {
            var seq = Enumerable.Range(0, 59);

            var pagedList = PaginationHelper.CreatePagedList(seq, 2, 10);

            Assert.Equal(Enumerable.Range(10, 10), pagedList);
        }

        [Fact]
        public void CreatePagedList_ParametersAreInvalid_ReturnsNull()
        {
            var seq = Enumerable.Range(0, 59);

            var pagedList = PaginationHelper.CreatePagedList(seq, 2, 100);

            Assert.Null(pagedList);
        }

        [Fact]
        public void CreatePagedList_ParametersAreInvalid_ReturnsNull2()
        {
            var seq = Enumerable.Range(0, 59);

            var pagedList = PaginationHelper.CreatePagedList(seq, 0, 100);

            Assert.Null(pagedList);
        }

        [Fact]
        public void CreatePagedList_ParametersAreInvalid_ReturnsNull3()
        {
            var seq = Enumerable.Range(0, 59);

            var pagedList = PaginationHelper.CreatePagedList(seq, 5, 0);

            Assert.Null(pagedList);
        }

        [Fact]
        public void CreateAllPages_ReturnsCorrectNumberOfPages()
        {
            var seq = Enumerable.Range(0, 59);

            var pagedList = PaginationHelper.CreateAllPages(seq, 10);

            Assert.Equal(6, pagedList.Count);
        }
    }
}