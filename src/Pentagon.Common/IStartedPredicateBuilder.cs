// -----------------------------------------------------------------------
//  <copyright file="IStartedPredicateBuilder.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon
{
    using System;
    using System.Linq.Expressions;

    public interface IStartedPredicateBuilder<T>
    {
        IStartedPredicateBuilder<T> And(Expression<Func<T, bool>> predicate);

        IStartedPredicateBuilder<T> Or(Expression<Func<T, bool>> predicate);

        IStartedPredicateBuilder<T> And(Action<IPredicateBuilder<T>> predicate);

        IStartedPredicateBuilder<T> Or(Action<IPredicateBuilder<T>> predicate);

        Expression<Func<T, bool>> Build();
    }
}