// -----------------------------------------------------------------------
//  <copyright file="IAnnotatedOperationResult.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.OperationResults
{
    using System.Collections.Generic;
    using JetBrains.Annotations;

    public interface IAnnotatedOperationResult
    {
        [NotNull]
        IReadOnlyList<ResultMessage> Messages { get; }

        void AddMessage([NotNull] string messageText);

        void AddMessage([NotNull] string messageText, string messageTitle);
    }
}