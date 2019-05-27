// -----------------------------------------------------------------------
//  <copyright file="TypeNameHelperTests.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Common.Tests.Helpers
{
    using System;
    using Pentagon.Helpers;
    using Xunit;

    public class TypeNameHelperTests
    {
        [Fact]
        public void GetTypeDisplayName2_FirstArgumentIsNull_ReturnsNull()
        {
            var s1 = TypeNameHelper.GetTypeDisplayName((object) null);
            var s2 = TypeNameHelper.GetTypeDisplayName((object) null, false);

            Assert.Null(s1);
            Assert.Null(s2);
        }

        [Theory]
        [InlineData("TestName",typeof(TestName),false,false,true,'+')]
        [InlineData("TestName", typeof(TestName), false, false, true, ':')]
        public void GetTypeDisplayName5_ReturnsCorrectName(string expected,
                                                           Type type,
                                                           bool fullName,
                                                           bool includeGenericParameterNames,
                                                           bool includeGenericParameters,
                                                           char nestedTypeDelimiter)
        {
            var name = TypeNameHelper.GetTypeDisplayName(type, fullName, includeGenericParameterNames, includeGenericParameters, nestedTypeDelimiter);

            Assert.Equal(expected, name);
        }

        [Fact]
        public void GetTypeDisplayName5_FirstArgumentIsNull_Throws()
        {
            Assert.ThrowsAny<Exception>(() => TypeNameHelper.GetTypeDisplayName(null));
        }

        class TestName { }

        class TestNameGeneric<T> { }
    }
}