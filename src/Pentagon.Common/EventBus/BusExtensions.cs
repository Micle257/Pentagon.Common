// -----------------------------------------------------------------------
//  <copyright file="BusExtensions.cs">
//   Copyright (c) Smartdata s. r. o. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.EventBus
{
    using System;

    public static class BusExtensions
    {
        public static Guid Subscribe<TMessage>(this IEventBus bus, Func<IHandle<TMessage>> handlerFactory)
        {
            return bus.Subscribe<TMessage>(message
                                                   => handlerFactory.Invoke().Handle(message));
        }

        public static Guid Subscribe<TMessage>(this IEventBus bus, Func<IHandleAsync<TMessage>> handlerFactory)
        {
            return bus.Subscribe<TMessage>((message, cancellationToken)
                                                   => handlerFactory.Invoke().HandleAsync(message, cancellationToken));
        }
    }
}