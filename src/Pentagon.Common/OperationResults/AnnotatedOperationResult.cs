// -----------------------------------------------------------------------
//  <copyright file="AnnotatedOperationResult.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.OperationResults
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Extensions;
    using JetBrains.Annotations;

    /// <summary> Represents an annotated <see cref="OperationResult" /> with messages. </summary>
    public class AnnotatedOperationResult : OperationResult, IAnnotatedOperationResult
    {
        /// <summary> The default message title. </summary>
        const string DefaultMessageTitle = "Message";

        /// <summary> The messages. </summary>
        [NotNull]
        List<ResultMessage> _messages = new List<ResultMessage>();

        /// <inheritdoc />
        public IReadOnlyList<ResultMessage> Messages
        {
            get => _messages;
            private set => _messages = value.ToList();
        }

        #region Operators

        /// <summary> Implements the operator +. </summary>
        /// <param name="op1"> The op1. </param>
        /// <param name="op2"> The op2. </param>
        /// <returns> The result of the operator. </returns>
        public static AnnotatedOperationResult operator +([NotNull] AnnotatedOperationResult op1, [NotNull] AnnotatedOperationResult op2)
        {
            if (op1 == null)
                throw new ArgumentNullException(nameof(op1));

            if (op2 == null)
                throw new ArgumentNullException(nameof(op2));

            var ex = new[] {op1.Exception, op2.Exception}.WhereNotNull().ToList();

            return new AnnotatedOperationResult
                   {
                           Exception = ex.Count == 0 ? null : ex.Count == 1 ? ex[0] : new AggregateException(ex),
                           Messages = op1.Messages.Union(op2.Messages).ToList()
                   };
        }

        #endregion

        /// <inheritdoc />
        public void AddMessage(string messageText)
        {
            _messages.Add(new ResultMessage(DefaultMessageTitle, messageText));
        }

        /// <inheritdoc />
        public void AddMessage(string messageText, string messageTitle)
        {
            var title = string.IsNullOrWhiteSpace(messageTitle) ? DefaultMessageTitle : messageTitle;

            _messages.Add(new ResultMessage(title, messageText));
        }
    }

    public class AnnotatedOperationResult<TContent> : AnnotatedOperationResult
    {
        public new static OperationResult<TContent> Success => new OperationResult<TContent>();

        public TContent Content { get; set; }

        public static OperationResult<TContent> FromSuccess(TContent content) =>
                new OperationResult<TContent>
                {
                        Content = content
                };

        public new static OperationResult<TContent> FromFailed(Exception exception) =>
                new OperationResult<TContent>
                {
                        Exception = exception
                };
    }
}