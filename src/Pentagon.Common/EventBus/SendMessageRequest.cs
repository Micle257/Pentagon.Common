// -----------------------------------------------------------------------
//  <copyright file="SendMessageRequest.cs">
//   Copyright (c) Smartdata s. r. o. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.EventBus
{
    using System;
    using System.Threading;

    sealed class SendMessageRequest
    {
        public SendMessageRequest(object payload, CancellationToken cancellationToken)
                : this(payload, cancellationToken, success => { }) { }

        public SendMessageRequest(object payload, CancellationToken cancellationToken, Action<bool> onSendComplete)
        {
            Payload = payload;
            CancellationToken = cancellationToken;
            OnSendComplete = onSendComplete;
        }

        public object Payload { get; }
        public CancellationToken CancellationToken { get; }
        public Action<bool> OnSendComplete { get; }
    }
}