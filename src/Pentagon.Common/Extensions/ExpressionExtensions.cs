﻿// -----------------------------------------------------------------------
//  <copyright file="ExpressionExtensions.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Extensions
{
    using System;
    using System.Linq.Expressions;
    using System.Reflection;
    using JetBrains.Annotations;

    /// <summary> Contains extension methods for <see cref="Expression" />. </summary>
    public static class ExpressionExtensions
    {
        /// <summary> Gets the value from property in expression of format {() => property}.
        ///     <para />
        ///     Equivalent to get value by reference (e.g. fields). </summary>
        /// <typeparam name="T"> The type of the value. </typeparam>
        /// <param name="expression"> The expression of the value. </param>
        /// <returns> A value from expression. </returns>
        public static T GetPropertyValue<T>(this Expression<Func<T>> expression) => expression.Compile()();

        /// <summary> Sets the value of property in expression of format {() => property}.
        ///     <para />
        ///     Equivalent to assign value by reference (e.g. fields). </summary>
        /// <typeparam name="T"> The type of the value. </typeparam>
        /// <param name="expression"> The expression of the value. </param>
        /// <param name="value"> The value to set. </param>
        public static void SetPropertyValue<T>(this Expression<Func<T>> expression, T value)
        {
            var memberEx = expression.Body as MemberExpression;

            var propertyInfo = (PropertyInfo) memberEx.Member;
            var target = Expression.Lambda(memberEx.Expression).Compile().DynamicInvoke();

            propertyInfo.SetValue(target, value);
        }

        /// <summary> Gets the value from the <see cref="MemberExpression" />. </summary>
        /// <param name="expression"> The expression. </param>
        /// <returns> A <see cref="Object" /> that represents value of member expression. </returns>
        public static object GetMemberValue([NotNull] this MemberExpression expression)
        {
            if (expression == null)
                throw new ArgumentNullException(nameof(expression));

            // casts the expression to object
            var objectMember = Expression.Convert(expression, typeof(object));

            // create a lambda from object expression
            var getterLambda = Expression.Lambda<Func<object>>(objectMember);

            return getterLambda.Compile()();
        }
    }
}