// -----------------------------------------------------------------------
//  <copyright file="Subscription.cs">
//   Copyright (c) Smartdata s. r. o. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.EventBus
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    sealed class Subscription
    {
        Subscription(Guid id, Func<object, CancellationToken, Task> handler)
        {
            Id = id;
            Handler = handler;
        }

        public Guid Id { get; }
        public Func<object, CancellationToken, Task> Handler { get; }

        public static Subscription Create<TMessage>(Func<TMessage, CancellationToken, Task> handler)
        {
            return Create(Guid.NewGuid(), handler);
        }

        public static Subscription Create<TMessage>(Guid id,Func<TMessage, CancellationToken, Task> handler)
        {
            Func<object, CancellationToken, Task> handlerWithCheck = async (message, cancellationToken) =>
                                                                     {
                                                                         if (message is TMessage msg)
                                                                             await handler(msg, cancellationToken);
                                                                     };

            return new Subscription(id, handlerWithCheck);
        }
    }
}