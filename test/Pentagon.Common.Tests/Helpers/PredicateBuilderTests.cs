// -----------------------------------------------------------------------
//  <copyright file="PredicateBuilderTests.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Common.Tests.Helpers
{
    using System;
    using System.Linq.Expressions;
    using Pentagon.Helpers;
    using Xunit;

    public class PredicateBuilderTests
    {
        [Fact]
        public void Start_WithPredicate_ConstructsValidExpression()
        {
            var builder = new PredicateBuilder<int>();

            var predicate = builder.Start(a => a == 3).Build().Compile();

            Assert.True(predicate(3));
            Assert.False(predicate(-4));
        }

        [Fact]
        public void Or_WithPredicate_ConstructsValidExpression()
        {
            var builder = new PredicateBuilder<int>();

            var predicate = builder.Start(a => a == 3)
                                   .Or(a => a == 4).Build().Compile();

            Assert.True(predicate(3));
            Assert.True(predicate(4));
            Assert.False(predicate(-4));
        }

        [Fact]
        public void And_WithPredicate_ConstructsValidExpression()
        {
            var builder = new PredicateBuilder<int>();

            var predicate = builder.Start(a => a > 3)
                                   .And(a => a <= 7).Build().Compile();

            Assert.True(predicate(4));
            Assert.True(predicate(7));
            Assert.False(predicate(3));
            Assert.False(predicate(8));
        }

        [Fact]
        public void And_WithBuilder_ConstructsValidExpression()
        {
            var builder = new PredicateBuilder<int>();

            var predicate = builder.Start(a => a > 5)
                                   .And(b => b.Start(a => a == 5)
                                              .Or(a => a == 7))
                                   .Build().Compile();

            Assert.True(predicate(7));
            Assert.False(predicate(5));
            Assert.False(predicate(2));
            Assert.False(predicate(8));
        }
    }
}