// -----------------------------------------------------------------------
//  <copyright file="BusExtensions.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.EventBus
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Threading;

    public static class BusExtensions
    {
        public static Task SendAsync<TMessage>(this IEventBus bus, TMessage message) => bus.SendAsync(message, CancellationToken.None);

        public static void Send<TMessage>(this IEventBus bus, TMessage message) => TaskHelper.RunAndForget(() => bus.SendAsync(message));

        public static Guid Subscribe<TMessage>(this IEventBus bus, Action<TMessage> handler, Action<SubscriptionFactoryOptions<TMessage>> configure = null)
        {
            return bus.Subscribe((message, cancellationToken) =>
                                 {
                                     handler(message);
                                     return Task.FromResult(0);
                                 },
                                 configure);
        }

        public static Guid Subscribe<TMessage>(this IEventBus bus, Func<TMessage,Task> handler, Action<SubscriptionFactoryOptions<TMessage>> configure = null)
        {
            return bus.Subscribe((message, cancellationToken) =>
                                 {
                                     handler(message);
                                     return Task.FromResult(0);
                                 },
                                 configure);
        }
    }
}