// -----------------------------------------------------------------------
//  <copyright file="OperationResult.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon
{
    using System;

    public class OperationResult
    {
        public static OperationResult Success => new OperationResult();

        public bool IsSuccessful => Exception == null;

        public Exception Exception { get; set; }

        public static OperationResult FromFailed(Exception exception) =>
                new OperationResult
                {
                        Exception = exception
                };
    }
}