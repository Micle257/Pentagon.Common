// -----------------------------------------------------------------------
//  <copyright file="AnnotatedOperationResultExtensions.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.OperationResults
{
    using System;
    using JetBrains.Annotations;

    public static class AnnotatedOperationResultExtensions
    {
        public static IAnnotatedOperationResult AddError<T>([NotNull] this IAnnotatedOperationResult result, [NotNull] T message)
        {
            if (result == null)
                throw new ArgumentNullException(nameof(result));

            result.AddMessage(message.ToString(), messageTitle: "Error");

            return result;
        }

        public static IAnnotatedOperationResult AddWarning<T>([NotNull] this IAnnotatedOperationResult result, T message)
        {
            if (result == null)
                throw new ArgumentNullException(nameof(result));

            result.AddMessage(message.ToString(), messageTitle: "Warning");

            return result;
        }

        public static IAnnotatedOperationResult AddSuccessMessage<T>([NotNull] this IAnnotatedOperationResult result, T message)
        {
            if (result == null)
                throw new ArgumentNullException(nameof(result));

            result.AddMessage(message.ToString(), messageTitle: "Success");

            return result;
        }
    }
}