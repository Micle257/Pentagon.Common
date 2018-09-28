// -----------------------------------------------------------------------
//  <copyright file="PredicateBuilder.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Helpers
{
    using System;
    using System.Linq.Expressions;

    public class PredicateBuilder<T> : IPredicateBuilder<T>
    {
        Expression _predicateBody;

        public IStartedPredicateBuilder<T> Start(Expression<Func<T, bool>> predicate)
        {
            _predicateBody = predicate.Body;

            return this;
        }

        public IStartedPredicateBuilder<T> And(Expression<Func<T, bool>> predicate)
        {
            _predicateBody = Expression.AndAlso(_predicateBody, predicate.Body);

            return this;
        }

        public IStartedPredicateBuilder<T> Or(Expression<Func<T, bool>> predicate)
        {
            _predicateBody = Expression.OrElse(_predicateBody, predicate.Body);

            return this;
        }

        public IStartedPredicateBuilder<T> And(Action<IPredicateBuilder<T>> predicate)
        {
            var builder = new PredicateBuilder<T>();

            predicate(builder);

            return And(builder.Build());
        }

        public IStartedPredicateBuilder<T> Or(Action<IPredicateBuilder<T>> predicate)
        {
            var builder = new PredicateBuilder<T>();

            predicate(builder);

            return Or(builder.Build());
        }

        public Expression<Func<T, bool>> Build()
        {
            var parameter = Expression.Parameter(typeof(T), name: "a");

            var body = ExpressionParameterReplacer.Replace(_predicateBody, parameter);

            return Expression.Lambda<Func<T, bool>>(body, parameter);
        }
    }
}