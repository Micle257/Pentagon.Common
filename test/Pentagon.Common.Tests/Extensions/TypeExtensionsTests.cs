// -----------------------------------------------------------------------
//  <copyright file="TypeExtensionsTests.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Common.Tests.Extensions
{
    using System.Linq;
    using Pentagon.Extensions;
    using Xunit;

    public class TypeExtensionsTests
    {
        [Fact]
        public void GetNullableType_CalledOnNullableType_ReturnsGenericType()
        {
            var type = typeof(int?).GetNullableType();

            Assert.Equal(typeof(int), type);
        }

        [Fact]
        public void GetNullableType_CalledOnNotNullableType_ReturnsNull()
        {
            var type = typeof(int).GetNullableType();

            Assert.Null(type);
        }

        [Fact]
        public void GetAutoProperties_CalledOnType_ReturnsAutoProperties()
        {
            var properties = typeof(TestClass).GetAutoProperties();

            var names = properties.Select(a => a.Name).ToArray();

            Assert.Contains(names, a => a == nameof(TestClass.Text));
            Assert.Contains(names, a => a == nameof(TestClass.Value));
        }

        [Fact]
        public void GetValue_CalledOnMemberInfo_ReturnsCorrectValue()
        {
            var obj = new TestClass();

            obj.Value = 2;

            var member = typeof(TestClass).GetMember(nameof(TestClass.Value)).FirstOrDefault();

            var value = (int) member.GetValue(obj);

            Assert.Equal(obj.Value, value);
        }

        class TestClass
        {
#pragma warning disable 169
            double _decay;
#pragma warning restore 169

            // ReSharper disable once UnusedMember.Local
            public static string St { get; set; }

            // ReSharper disable once UnusedMember.Local
            // ReSharper disable once UnassignedGetOnlyAutoProperty
            public char Get { get; }
            public int Value { get; set; }

            // ReSharper disable once UnusedAutoPropertyAccessor.Local
            public string Text { get; set; }

            // ReSharper disable once UnusedMember.Local
            int Value2 { get; set; }
        }
    }
}