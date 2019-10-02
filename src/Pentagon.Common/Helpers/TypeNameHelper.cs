// -----------------------------------------------------------------------
//  <copyright file="TypeNameHelper.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Text;
    using JetBrains.Annotations;

    /// <summary>
    /// Provides logic for displaying type name's in more controlled and pretty way.
    /// </summary>
    public class TypeNameHelper
    {
        /// <summary>
        /// The default nested type delimiter.
        /// </summary>
        const char DefaultNestedTypeDelimiter = '+';

        /// <summary>
        /// The built in type names.
        /// </summary>
        static readonly Dictionary<Type, string> BuiltInTypeNames = new Dictionary<Type, string>
                                                                     {
                                                                             {typeof(void), "void"},
                                                                             {typeof(bool), "bool"},
                                                                             {typeof(byte), "byte"},
                                                                             {typeof(char), "char"},
                                                                             {typeof(decimal), "decimal"},
                                                                             {typeof(double), "double"},
                                                                             {typeof(float), "float"},
                                                                             {typeof(int), "int"},
                                                                             {typeof(long), "long"},
                                                                             {typeof(object), "object"},
                                                                             {typeof(sbyte), "sbyte"},
                                                                             {typeof(short), "short"},
                                                                             {typeof(string), "string"},
                                                                             {typeof(uint), "uint"},
                                                                             {typeof(ulong), "ulong"},
                                                                             {typeof(ushort), "ushort"}
                                                                     };

        /// <summary>
        /// Gets the display name of the type.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="fullName">if set to <c>true</c> result will contain full name.</param>
        /// <returns>The pretty printed type name.</returns>
        [CanBeNull]
        public static string GetTypeDisplayName([CanBeNull] object item, bool fullName = true) => item == null ? null : GetTypeDisplayName(item.GetType(), fullName);

        /// <summary> Pretty print a type name. </summary>
        /// <param name="type"> The <see cref="Type" />. </param>
        /// <param name="fullName"> <c> true </c> to print a fully qualified name. </param>
        /// <param name="includeGenericParameterNames"> <c> true </c> to include generic parameter names. </param>
        /// <param name="includeGenericParameters"> <c> true </c> to include generic parameters. </param>
        /// <param name="nestedTypeDelimiter"> Character to use as a delimiter in nested type names </param>
        /// <returns> The pretty printed type name. </returns>
        public static string GetTypeDisplayName([JetBrains.Annotations.NotNull] Type type,
                                                bool fullName = true,
                                                bool includeGenericParameterNames = false,
                                                bool includeGenericParameters = true,
                                                char nestedTypeDelimiter = DefaultNestedTypeDelimiter)
        {
            if (type == null)
                throw new ArgumentNullException(nameof(type));

            var builder = new StringBuilder();

            ProcessType(builder, type, new DisplayNameOptions(fullName, includeGenericParameterNames, includeGenericParameters, nestedTypeDelimiter));

            return builder.ToString();
        }

        static void ProcessType(StringBuilder builder, Type type, in DisplayNameOptions options)
        {
            if (type.IsGenericType)
            {
                var genericArguments = type.GetGenericArguments();
                ProcessGenericType(builder, type, genericArguments, genericArguments.Length, options);
            }
            else if (type.IsArray)
                ProcessArrayType(builder, type, options);
            else if (BuiltInTypeNames.TryGetValue(type, out var builtInName))
                builder.Append(builtInName);
            else if (type.IsGenericParameter)
            {
                if (options.IncludeGenericParameterNames)
                    builder.Append(type.Name);
            }
            else
            {
                var name = options.FullName ? type.FullName : type.Name;
                builder.Append(name);

                if (options.NestedTypeDelimiter != DefaultNestedTypeDelimiter)
                    builder.Replace(DefaultNestedTypeDelimiter, options.NestedTypeDelimiter, builder.Length - name.Length, name.Length);
            }
        }

        static void ProcessArrayType(StringBuilder builder, Type type, in DisplayNameOptions options)
        {
            var innerType = type;
            while (innerType.IsArray)
                innerType = innerType.GetElementType();

            ProcessType(builder, innerType, options);

            while (type.IsArray)
            {
                builder.Append('[');
                builder.Append(',', type.GetArrayRank() - 1);
                builder.Append(']');
                type = type.GetElementType();
            }
        }

        /// <summary>
        /// Processes the type of the generic.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <param name="type">The type.</param>
        /// <param name="genericArguments">The generic arguments.</param>
        /// <param name="length">The length.</param>
        /// <param name="options">The options.</param>
        [SuppressMessage("ReSharper", "WarningHighlighting")]
        static void ProcessGenericType(StringBuilder builder, Type type, Type[] genericArguments, int length, in DisplayNameOptions options)
        {
            var offset = 0;
            if (type.IsNested)
                offset = type.DeclaringType.GetGenericArguments().Length;

            if (options.FullName)
            {
                if (type.IsNested)
                {
                    ProcessGenericType(builder, type.DeclaringType, genericArguments, offset, options);
                    builder.Append(options.NestedTypeDelimiter);
                }
                else if (!string.IsNullOrEmpty(type.Namespace))
                {
                    builder.Append(type.Namespace);
                    builder.Append('.');
                }
            }

            var genericPartIndex = type.Name.IndexOf('`');
            if (genericPartIndex <= 0)
            {
                builder.Append(type.Name);
                return;
            }

            builder.Append(type.Name, 0, genericPartIndex);

            if (options.IncludeGenericParameters)
            {
                builder.Append('<');
                for (var i = offset; i < length; i++)
                {
                    ProcessType(builder, genericArguments[i], options);
                    if (i + 1 == length)
                        continue;

                    builder.Append(',');
                    if (options.IncludeGenericParameterNames || !genericArguments[i + 1].IsGenericParameter)
                        builder.Append(' ');
                }

                builder.Append('>');
            }
        }

        readonly struct DisplayNameOptions
        {
            public DisplayNameOptions(bool fullName, bool includeGenericParameterNames, bool includeGenericParameters, char nestedTypeDelimiter)
            {
                FullName = fullName;
                IncludeGenericParameters = includeGenericParameters;
                IncludeGenericParameterNames = includeGenericParameterNames;
                NestedTypeDelimiter = nestedTypeDelimiter;
            }

            public bool FullName { get; }

            public bool IncludeGenericParameters { get; }

            public bool IncludeGenericParameterNames { get; }

            public char NestedTypeDelimiter { get; }
        }
    }
}