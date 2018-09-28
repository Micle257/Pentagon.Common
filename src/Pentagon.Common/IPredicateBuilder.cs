// -----------------------------------------------------------------------
//  <copyright file="IPredicateBuilder.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon
{
    using System;
    using System.Linq.Expressions;
    using Helpers;

    public interface IPredicateBuilder<T> : IStartedPredicateBuilder<T>
    {
        IStartedPredicateBuilder<T> Start(Expression<Func<T, bool>> predicate);
    }
}