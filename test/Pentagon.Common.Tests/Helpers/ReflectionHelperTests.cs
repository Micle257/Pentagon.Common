// -----------------------------------------------------------------------
//  <copyright file="ReflectionHelperTests.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Common.Tests.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Pentagon.Helpers;
    using Xunit;

    public class ReflectionHelperTests
    {
        [Fact]
        public void GetPublicConstants_ReturnsCorrectNumberOfItems()
        {
            var fields = ReflectionHelper.GetPublicConstants(typeof(TestClass));

            Assert.Equal(3, fields.Count);
        }

        [Fact]
        public void GetPublicConstants_ReturnsCorrectItems()
        {
            var fields = ReflectionHelper.GetPublicConstants(typeof(TestClass));

            Assert.True(fields.Any(a => a.Name == "First"));
            Assert.True(fields.Any(a => a.Name == "Second"));
            Assert.True(fields.Any(a => a.Name == "Test"));
        }

        [Fact]
        public void GetPublicConstants_ReturnsCorrectItemsInOrder()
        {
            var fields = ReflectionHelper.GetPublicConstants(typeof(TestClass));

            Assert.Equal("First", fields[0].Name);
            Assert.Equal("Second", fields[1].Name);
            Assert.Equal("Test", fields[2].Name);
        }

        [Fact]
        public void GetPublicConstants_TypeIsNull_Throws()
        {
            Assert.Throws<ArgumentNullException>(() => ReflectionHelper.GetPublicConstants(null));
        }

        [Fact]
        public void GetAllPublicConstantValues_ReturnsCorrectNumberOfItems()
        {
            var values = ReflectionHelper.GetAllPublicConstantValues<int>(typeof(TestClass));

            Assert.Equal(2, values.Count);
        }

        [Fact]
        public void GetAllPublicConstantValues_ReturnsCorrectItems()
        {
            var fields = ReflectionHelper.GetAllPublicConstantValues<int>(typeof(TestClass));

            Assert.True(fields.Any(a => a == 1));
            Assert.True(fields.Any(a => a == 2));
        }

        [Fact]
        public void GetAllPublicConstantValues_ReturnsCorrectItemsInOrder()
        {
            var fields = ReflectionHelper.GetAllPublicConstantValues<int>(typeof(TestClass));

            Assert.Equal(1, fields[0]);
            Assert.Equal(2, fields[1]);
        }

        [Fact]
        public void GetAllPublicConstantValues_TypeIsNull_Throws()
        {
            Assert.Throws<ArgumentNullException>(() => ReflectionHelper.GetAllPublicConstantValues<object>(null));
        }

        class TestClass
        {
            public const int First = 1;

            public const int Second = 2;

            const int Third = 3;

            public const string Test = "test";

            const string Foo = "bar";
        }
    }
}