// -----------------------------------------------------------------------
//  <copyright file="ExpressionParameterReplacer.cs">
//   Copyright (c) Michal Pokorn�. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Helpers
{
    using System.Linq.Expressions;

    public static class ExpressionParameterReplacer
    {
        public static Expression Replace(Expression expression,
                                         ParameterExpression source,
                                         Expression target) => new ParameterReplacerVisitor(source, target).Visit(expression);

        public static Expression Replace(Expression expression,
                                         ParameterExpression source) => new SimpleParameterReplacerVisitor(source).Visit(expression);

        class ParameterReplacerVisitor : ExpressionVisitor
        {
            readonly ParameterExpression _source;
            readonly Expression _target;

            public ParameterReplacerVisitor(ParameterExpression source, Expression target)
            {
                _source = source;
                _target = target;
            }

            protected override Expression VisitParameter(ParameterExpression node) => node == _source ? _target : base.VisitParameter(node);
        }

        class SimpleParameterReplacerVisitor : ExpressionVisitor
        {
            readonly ParameterExpression _source;

            public SimpleParameterReplacerVisitor(ParameterExpression source)
            {
                _source = source;
            }

            protected override Expression VisitParameter(ParameterExpression node) => base.VisitParameter(_source);
        }
    }
}