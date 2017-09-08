// -----------------------------------------------------------------------
//  <copyright file="AttributeExtensions.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Extensions
{
    using System;
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
            if (valueSelector == null)
                throw new ArgumentNullException(nameof(valueSelector));
            var att = type.GetTypeInfo().GetCustomAttributes(typeof(TAttribute), true).FirstOrDefault() as TAttribute;
            return att != null ? valueSelector(att) : default(TValue);
        }

        /// <summary> Determines whether the specified name of value's property has given attribute. </summary>
        /// <typeparam name="T"> The type of attribute. </typeparam>
        /// <param name="value"> The value object. </param>
        /// <param name="name"> The name of the property. </param>
        /// <returns>
        ///     <c> true </c> if the specified name is attribute; otherwise, <c> false </c>. </returns>
        public static bool IsAttribute<T>([NotNull] this object value, string name)
            where T : Attribute
        {
            Require.NotNull(() => value);
            var att = value.GetType().GetRuntimeProperties().FirstOrDefault(a => a?.Name == name).GetCustomAttributes<T>(true).FirstOrDefault();
            return att != null;
        }

        /// <summary> Determines whether the specified value has given attribute. </summary>
        /// <typeparam name="T"> The type of attribute. </typeparam>
        /// <param name="value"> The value object. </param>
        /// <returns>
        ///     <c> true </c> if the value has attribute; otherwise, <c> false </c>. </returns>
        public static bool HasAttribute<T>([NotNull] this object value)
            where T : Attribute 
            => value.GetType().GetTypeInfo().GetCustomAttribute<T>() != null;

        /// <summary> Gets the attribute property value from type's property. </summary>
        /// <typeparam name="TType"> The type of the type which contains the property. </typeparam>
        /// <typeparam name="TProperty"> The type of the property. </typeparam>
        /// <typeparam name="TAttribute"> The type of the attribute. </typeparam>
        /// <typeparam name="TValue"> The type of the attribute property. </typeparam>
        /// <param name="propertyExpression"> The property expression (<typeparamref name="TType" /> => <typeparamref name="TProperty" />). </param>
        /// <param name="valueSelector"> The attribute's value selector. </param>
        /// <returns> A value from selected attribute property. </returns>
        public static TValue GetPropertyAttributeValue<TType, TProperty, TAttribute, TValue>(
            [NotNull] Expression<Func<TType, TProperty>> propertyExpression,
            [NotNull] Func<TAttribute, TValue> valueSelector)
            where TAttribute : Attribute
        {
            Require.NotNull(() => propertyExpression);
            Require.NotNull(() => valueSelector);
            var expression = (MemberExpression) propertyExpression.Body;
            var propertyInfo = (PropertyInfo) expression?.Member;
            var att = propertyInfo.GetCustomAttributes(typeof(TAttribute), true).FirstOrDefault() as TAttribute;
            return att != null ? valueSelector(att) : default(TValue);
        }
    }
}