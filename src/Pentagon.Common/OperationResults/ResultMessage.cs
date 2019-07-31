// -----------------------------------------------------------------------
//  <copyright file="ResultMessage.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.OperationResults
{
    using System;
    using JetBrains.Annotations;

    /// <summary> Represents a message in <see cref="IAnnotatedOperationResult" />. </summary>
    public struct ResultMessage
    {
        /// <summary> Initializes a new instance of the <see cref="ResultMessage" /> struct. </summary>
        /// <param name="title"> The title of the message. </param>
        /// <param name="content"> The text content of the message. </param>
        /// <exception cref="ArgumentNullException"> title or content </exception>
        public ResultMessage([NotNull] string title, [NotNull] string content)
        {
            Title = title ?? throw new ArgumentNullException(nameof(title));
            Content = content ?? throw new ArgumentNullException(nameof(content));
        }

        /// <summary> Gets the title of the message. </summary>
        /// <value> The non-null <see cref="string" />. </value>
        [NotNull]
        public string Title { get; }

        /// <summary> Gets the text content. </summary>
        /// <value> The non-null <see cref="string" />. </value>
        [NotNull]
        public string Content { get; }
    }
}