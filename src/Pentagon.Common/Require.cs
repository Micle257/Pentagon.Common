// -----------------------------------------------------------------------
//  <copyright file="Require.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Exceptions;
    using Helpers;
    using JetBrains.Annotations;

    /// <summary> Provides validation logic methods. </summary>
    public static class Require
    {
        /// <summary> Gets or sets a value indicating whether require methods should throw exception when the argument is not valid. </summary>
        /// <value> <c> true </c> if throw exceptions; otherwise, <c> false </c>. </value>
        [UsedImplicitly]
        public static bool ThrowExceptions { get; set; } = true;

        /// <summary> Requires that <see cref="string" /> is not null and don't contains just whitespace. </summary>
        /// <param name="value"> The value. </param>
        /// <exception cref="StringArgumentException"> When value is not valid. </exception>
        [UsedImplicitly]
        public static IRequireResult StringNotNullNorWhiteSpace(string value)
        {
            var result = new RequireResult<StringArgumentException>();

            if (string.IsNullOrWhiteSpace(value))
            {
                result.Exception = new StringArgumentException(error: "not null or whitespace");
                if (ThrowExceptions)
                    throw result.Exception;
            }

            return result;
        }

        /// <summary> Requires that some value is in range. </summary>
        /// <typeparam name="T"> Type of the value. </typeparam>
        /// <param name="valueExpression"> The value expression. </param>
        /// <param name="range"> The range. </param>
        /// <exception cref="ValueOutOfRangeException{T}"> When value is not valid. </exception>
        [UsedImplicitly]
        public static IRequireResult InRange<T>([NotNull] Expression<Func<T>> valueExpression, [NotNull] IRange<T> range)
            where T : IComparable<T>
        {
            var result = new RequireResult<ValueOutOfRangeException<T>>();

            var valueName = (valueExpression.Body as MemberExpression)?.Member?.Name;

            var compile = valueExpression.Compile();
            if (compile == null)
                throw new ArgumentException(message: "The given expression is not valid.");
            var value = compile();

            if (!range.InRange(value))
            {
                result.Exception = new ValueOutOfRangeException<T>(valueName ?? "value", value, range);
                if (ThrowExceptions)
                    throw result.Exception;
            }

            return result;
        }

        /// <summary> Requires that reference instance is not null. </summary>
        /// <typeparam name="T"> The type of the value. </typeparam>
        /// <param name="value"> The value encapsulated in expression of format '() =&gt; val'. </param>
        /// <param name="message"> The message. </param>
        /// <returns> A <see cref="IRequireResult" /> containing data about this require. </returns>
        /// <exception cref="ArgumentNullException"> When expression's member is null. </exception>
        [NotNull]
        [UsedImplicitly]
        public static IRequireResult NotNull<T>([NotNull] Expression<Func<T>> value, string message = "") // () => x
            where T : class
        {
            var result = new RequireResult<ArgumentNullException>();

            var valueName = (value.Body as MemberExpression)?.Member?.Name;
            var exactValue = value.Compile()();

            if (exactValue == null)
            {
                result.Exception = new ArgumentNullException(valueName ?? "value", message);
                if (ThrowExceptions)
                    throw result.Exception;
            }

            return result;
        }

        /// <summary> Requires that arguments matches the type. </summary>
        /// <typeparam name="TIs"> The required type. </typeparam>
        /// <param name="value"> The value. </param>
        /// <returns> A <see cref="IRequireResult" /> containing data about this require. </returns>
        /// <exception cref="ArgumentException"> When value is not valid. </exception>
        [NotNull]
        [UsedImplicitly]
        public static IRequireResult IsType<TIs>(object value)
        {
            var result = new RequireResult<ArgumentException>();

            if (!(value is TIs))
            {
                result.Exception = new ArgumentException($"Argument must be of type {typeof(TIs).Name}");
                if (ThrowExceptions)
                    throw result.Exception;
            }

            return result;
        }

        /// <summary> Requires that arguments matches the type. </summary>
        /// <typeparam name="TIs"> The required type. </typeparam>
        /// <param name="valueExpression"> The value expression. </param>
        /// <param name="castValue"> The output cast value. </param>
        /// <returns> A <see cref="IRequireResult" /> containing data about this require. </returns>
        /// <exception cref="ArgumentException"> When value is not valid. </exception>
        [NotNull]
        [UsedImplicitly]
        public static IRequireResult IsType<TIs>(Expression<Func<object>> valueExpression, out TIs castValue)
        {
            castValue = default(TIs);
            var result = new RequireResult<ArgumentException>();

            var valueName = (valueExpression.Body as MemberExpression)?.Member?.Name;

            var exactValue = valueExpression.Compile()();

            if (!(exactValue is TIs v))
            {
                result.Exception = new ArgumentException($"Argument must be of type {typeof(TIs).Name}", valueName ?? "value");

                if (ThrowExceptions)
                    throw result.Exception;
            }
            else
                castValue = v;

            return result;
        }

        /// <summary> Requires that reference instance is not default. </summary>
        /// <typeparam name="T"> Type of the value. </typeparam>
        /// <param name="valueExpression"> The value expression. </param>
        /// <returns> A <see cref="IRequireResult" /> containing data about this require. </returns>
        /// <exception cref="ArgumentNullException"> When value is not valid. </exception>
        [NotNull]
        [UsedImplicitly]
        public static IRequireResult NotDefault<T>([NotNull] Expression<Func<T>> valueExpression)
        {
            var result = new RequireResult<ArgumentNullException>();

            var valueName = (valueExpression.Body as MemberExpression)?.Member?.Name;

            var exactValue = valueExpression.Compile()();

            if (EqualityComparer<T>.Default.Equals(exactValue, default(T)))
            {
                result.Exception = new ArgumentNullException(valueName ?? "value", $"Parameter {valueName} cannot be null.");
                if (ThrowExceptions)
                    throw result.Exception;
            }

            return result;
        }

        /// <summary> Requires that all elements in a collection are not default value. </summary>
        /// <typeparam name="T"> Type of the value. </typeparam>
        /// <param name="items"> The items. </param>
        /// <exception cref="ArgumentNullException"> When any of items is not valid. </exception>
        [NotNull]
        [UsedImplicitly]
        public static IEnumerable<IRequireResult> ItemsNotDefault<T>([NotNull] IEnumerable<T> items)
        {
            foreach (var item in items)
                yield return NotDefault(() => item);
        }

        /// <summary> Requires that specified condition must be true. </summary>
        /// <typeparam name="T"> The type of the value. </typeparam>
        /// <param name="valueExpression"> The value expression. </param>
        /// <param name="conditionPredicate"> The condition predicate. </param>
        /// <param name="message"> The message. </param>
        /// <returns> A <see cref="IRequireResult" /> containing data about this require. </returns>
        /// <exception cref="System.ArgumentException"> The given expression is not valid. </exception>
        [NotNull]
        [UsedImplicitly]
        public static IRequireResult Condition<T>([NotNull] Expression<Func<T>> valueExpression, Func<T, bool> conditionPredicate, string message = null)
        {
            if (message == null)
                message = $"The condition {conditionPredicate} is not true";

            var result = new RequireResult<ArgumentException>();

            var valueName = (valueExpression.Body as MemberExpression)?.Member?.Name;
            // if (valueName == null)
            //     throw new ArgumentException("The given expression is not valid.");
            var exactValue = valueExpression.Compile()();

            if (!conditionPredicate(exactValue))
            {
                result.Exception = new ArgumentException(message, valueName ?? "value");
                if (ThrowExceptions)
                    throw result.Exception;
            }

            return result;
        }

        /// <summary> Requires that specified condition must be true. </summary>
        /// <param name="conditionPredicate"> The condition predicate. </param>
        /// <param name="message"> The message. </param>
        /// <returns> A <see cref="IRequireResult" /> containing data about this require. </returns>
        /// <exception cref="System.ArgumentException"> The given expression is not valid. </exception>
        [NotNull]
        [UsedImplicitly]
        public static IRequireResult Condition([NotNull] Func<bool> conditionPredicate, string message = null)
        {
            if (message == null)
                message = $"The condition {conditionPredicate} is not true";

            var result = new RequireResult<ArgumentException>();

            if (!conditionPredicate())
            {
                result.Exception = new ArgumentException(message);
                if (ThrowExceptions)
                    throw result.Exception;
            }

            return result;
        }
    }
}