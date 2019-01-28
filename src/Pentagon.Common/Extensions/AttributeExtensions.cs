// -----------------------------------------------------------------------
//  <copyright file="AttributeExtensions.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using JetBrains.Annotations;

    /// <summary> Contains extension methods for <see cref="Attribute" />. </summary>
    public static class AttributeExtensions
    {
        /// <summary> Gets an attribute value of this type. </summary>
        /// <typeparam name="TAttribute"> The type of the attribute. </typeparam>
        /// <typeparam name="TValue"> The type of the value. </typeparam>
        /// <param name="type"> The type. </param>
        /// <param name="valueSelector"> The value selector. </param>
        /// <returns> Selected value or <c> null </c>. </returns>
        /// <exception cref="ArgumentNullException"> <paramref name="valueSelector" /> is <c> null </c>. </exception>
        public static TValue GetAttributeValue<TAttribute, TValue>([NotNull] this Type type, [NotNull] Func<TAttribute, TValue> valueSelector)
            where TAttribute : Attribute
        {
            if (type == null)
                throw new ArgumentNullException(nameof(type));

            if (valueSelector == null)
                throw new ArgumentNullException(nameof(valueSelector));

            var att = type.GetTypeInfo().GetCustomAttributes(typeof(TAttribute), true).FirstOrDefault() as TAttribute;
            return att != null ? valueSelector(att) : default(TValue);
        }

        /// <summary> Determines whether the specified value has given attribute. </summary>
        /// <typeparam name="TAttribute"> The type of attribute. </typeparam>
        /// <param name="value"> The value object. </param>
        /// <returns> <c> true </c> if the value has attribute; otherwise, <c> false </c>. </returns>
        public static bool HasAttribute<TAttribute>([NotNull] this object value)
            where TAttribute : Attribute
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            return value.GetType().GetTypeInfo()?.GetCustomAttribute<TAttribute>() != null;
        }

        /// <summary> Gets the attribute property value from type's property. </summary>
        /// <typeparam name="TType"> The type of the type which contains the property. </typeparam>
        /// <typeparam name="TProperty"> The type of the property. </typeparam>
        /// <typeparam name="TAttribute"> The type of the attribute. </typeparam>
        /// <typeparam name="TValue"> The type of the attribute property. </typeparam>
        /// <param name="value"> The value. </param>
        /// <param name="propertyExpression"> The property expression (<typeparamref name="TType" /> =&gt; <typeparamref name="TProperty" />). </param>
        /// <param name="valueSelector"> The attribute's value selector. </param>
        /// <returns> A value from selected attribute property. </returns>
        public static TValue GetPropertyAttributeValue<TType, TProperty, TAttribute, TValue>([NotNull] this TType value,
                                                                                             [NotNull] Expression<Func<TType, TProperty>> propertyExpression,
                                                                                             [NotNull] Func<TAttribute, TValue> valueSelector)
            where TAttribute : Attribute
        {
            if (EqualityComparer<TType>.Default.Equals(value, default))
                throw new ArgumentNullException(nameof(value));

            if (propertyExpression == null)
                throw new ArgumentNullException(nameof(propertyExpression));

            if (valueSelector == null)
                throw new ArgumentNullException(nameof(valueSelector));

            var expression = (MemberExpression) propertyExpression.Body;
            var propertyInfo = (PropertyInfo) expression.Member;
            var att = propertyInfo.GetCustomAttributes(typeof(TAttribute), true).FirstOrDefault() as TAttribute;
            return att != null ? valueSelector(att) : default(TValue);
        }
    }
}