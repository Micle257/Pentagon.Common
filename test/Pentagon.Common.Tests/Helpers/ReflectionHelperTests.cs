// -----------------------------------------------------------------------
//  <copyright file="ReflectionHelperTests.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Common.Tests.Helpers
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
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

            Assert.Contains(expected: "First", fields.Select(a => a.Name));
            Assert.Contains(expected: "Second", fields.Select(a => a.Name));
            Assert.Contains(expected: "Test", fields.Select(a => a.Name));
        }

        [Fact]
        public void GetPublicConstants_ReturnsCorrectItemsInOrder()
        {
            var fields = ReflectionHelper.GetPublicConstants(typeof(TestClass));

            Assert.Equal(expected: "First", fields[0].Name);
            Assert.Equal(expected: "Second", fields[1].Name);
            Assert.Equal(expected: "Test", fields[2].Name);
        }

        [Fact]
        [SuppressMessage(category: "ReSharper", checkId: "AssignNullToNotNullAttribute")]
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

            Assert.Contains(1, fields);
            Assert.Contains(2, fields);
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

        [SuppressMessage(category: "ReSharper", checkId: "UnusedMember.Local", Justification = "Testing reflection")]
        class TestClass
        {
            public const int First = 1;

            public const int Second = 2;

            public const string Test = "test";

            const int Third = 3;

            const string Foo = "bar";
        }
    }
}