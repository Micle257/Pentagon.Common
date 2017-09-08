// -----------------------------------------------------------------------
//  <copyright file="FeedbackEventArgs.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.EventArguments
{
    using System;

    /// <summary> Provides argument data for user feedback. </summary>
    public class FeedbackEventArgs : EventArgs
    {
        /// <summary> Initializes a new instance of the <see cref="FeedbackEventArgs" /> class. </summary>
        /// <param name="message"> The message. </param>
        public FeedbackEventArgs(string message)
        {
            Message = message;
        }

        /// <summary> Gets or sets the message. </summary>
        /// <value> The <see cref="String" />. </value>
        public string Message { get; }

        /// <summary> Gets or sets a value indicating whether the feedback has been accepted. </summary>
        /// <value>
        ///     <c> true </c> if this instance is posted back; otherwise, <c> false </c>. </value>
        public bool IsPostedBack { get; set; }
    }
}