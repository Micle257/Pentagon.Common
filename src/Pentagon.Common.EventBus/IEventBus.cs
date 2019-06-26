// -----------------------------------------------------------------------
//  <copyright file="IEventBus.cs">
//   Copyright (c) Smartdata s. r. o. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.EventBus
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    public interface IEventBus
    {
        Task SendAsync<TMessage>(TMessage message, CancellationToken cancellationToken);

        Guid Subscribe<TMessage>(Func<TMessage, CancellationToken, Task> handler, Action<SubscriptionFactoryOptions<TMessage>> configure = null);

        void Unsubscribe(Guid subscriptionId);
    }
}