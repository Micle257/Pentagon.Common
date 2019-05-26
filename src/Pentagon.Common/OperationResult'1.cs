// -----------------------------------------------------------------------
//  <copyright file="OperationResult'1.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon
{
    using System;

    public class OperationResult<TContent> : OperationResult
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