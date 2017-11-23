// -----------------------------------------------------------------------
//  <copyright file="AttributeExtensionsTests.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Common.Tests.Extensions
{
    using System;
    using Pentagon.Extensions;
    using Xunit;

    public class AttributeExtensionsTests
    {
        [Fact]
        public void GetAttributeValue_ParameterValueIsNull_Throws()
        {
            Assert.Throws<ArgumentNullException>(() => AttributeExtensions.GetAttributeValue<TestedAttribute, bool>(null, a => true));
        }

        [Fact]
        public void GetAttributeValue_ParameterValueSelectorIsNull_Throws()
        {
            Assert.Throws<ArgumentNullException>(() => typeof(Stub).GetAttributeValue<TestedAttribute, bool>(null));
        }

        [Fact]
        public void GetAttributeValue_PassedCorrectParameters_ReturnsCorrectValue()
        {
            var type = typeof(Stub);

            var result = type.GetAttributeValue<TestedAttribute, bool>(attribute => attribute.IsActive);

            Assert.False(result);
        }

        [Fact]
        public void HasAttribute_ParameterValueIsNull_Throws()
        {
            Assert.Throws<ArgumentNullException>(() => AttributeExtensions.HasAttribute<TestedAttribute>(null));
        }

        [Fact]
        public void HasAttribute_PassedCorrectParameters_ReturnsCorrectValue()
        {
            var f = new Stub();

            Assert.True(f.HasAttribute<TestedAttribute>());
        }

        [Fact]
        public void GetPropertyAttributeValue_ParameterValueIsNull_Throws()
        {
            Assert.Throws<ArgumentNullException>(() => ((Stub) null).GetPropertyAttributeValue(foo => foo.Count, (TestedAttribute attribute) => attribute.IsActive));
        }

        [Fact]
        public void GetPropertyAttributeValue_ParameterPropertyExpressionIsNull_Throws()
        {
            var obj = new Stub {Count = 5};

            Assert.Throws<ArgumentNullException>(() => obj.GetPropertyAttributeValue<Stub, int, TestedAttribute, bool>(null, attribute => attribute.IsActive));
        }

        [Fact]
        public void GetPropertyAttributeValue_ParameterValueSelectorIsNull_Throws()
        {
            var obj = new Stub {Count = 5};

            Assert.Throws<ArgumentNullException>(() => obj.GetPropertyAttributeValue<Stub, int, TestedAttribute, bool>(foo => foo.Count, null));
        }

        [Fact]
        public void GetPropertyAttributeValue_PassedCorrectParameters_ReturnsCorrectAttributeValue()
        {
            var obj = new Stub {Count = 5};

            var value = obj.GetPropertyAttributeValue(foo => foo.Count, (TestedAttribute attribute) => attribute.IsActive);

            Assert.True(value);
        }

        [Tested(false)]
        class Stub
        {
            [Tested(true)]
            public int Count { get; set; }
        }

        class TestedAttribute : Attribute
        {
            public TestedAttribute(bool isActive)
            {
                IsActive = isActive;
            }

            public bool IsActive { get; }
        }
    }
}