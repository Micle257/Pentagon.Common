// -----------------------------------------------------------------------
//  <copyright file="InProcessBus.cs">
//   Copyright (c) Smartdata s. r. o. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.EventBus
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Threading.Tasks.Dataflow;
    using JetBrains.Annotations;
    using Threading;

    public class InProcessBus : IEventBus
    {
        readonly bool _defaultRunInBackground;

        [NotNull]
        readonly ConcurrentQueue<Subscription> _subscriptionRequests = new ConcurrentQueue<Subscription>();

        [NotNull]
        readonly ConcurrentQueue<Guid> _unsubscribeRequests = new ConcurrentQueue<Guid>();

        readonly ActionBlock<SendMessageRequest> _messageProcessor;

        public InProcessBus(bool defaultRunInBackground = false)
        {
            _defaultRunInBackground = defaultRunInBackground;

            // Only ever accessed from (single threaded) ActionBlock, so it is thread safe
            var subscriptions = new List<Subscription>();

            _messageProcessor = new ActionBlock<SendMessageRequest>(async request =>
                                                                   {
                                                                       // Process unsubscribe requests
                                                                       while (_unsubscribeRequests.TryDequeue(out var subscriptionId))
                                                                       {
                                                                           var id = subscriptionId;
                                                                           subscriptions.RemoveAll(s => s.Id == id);
                                                                       }

                                                                       // Process subscribe requests
                                                                       while (_subscriptionRequests.TryDequeue(out var newSubscription))
                                                                           subscriptions.Add(newSubscription);

                                                                       var result = true;

                                                                       foreach (var subscription in subscriptions)
                                                                       {
                                                                           if (request.CancellationToken.IsCancellationRequested)
                                                                           {
                                                                               result = false;
                                                                               break;
                                                                           }

                                                                           try
                                                                           {
                                                                               if (!subscription.IncludeDerivedTypes)
                                                                               {
                                                                                   if (subscription.MessageType != request.Payload.GetType())
                                                                                       continue;
                                                                               }
                                                                               else
                                                                               {
                                                                                   if (!request.Payload.GetType().IsSubclassOf(subscription.MessageType) && request.Payload.GetType() != subscription.MessageType)
                                                                                       continue;
                                                                               }

                                                                               if (subscription.RunDetached)
                                                                                   TaskHelper.RunAndForget(() => subscription.Handler(request.Payload, request.CancellationToken));
                                                                               else
                                                                                   await subscription.Handler(request.Payload, request.CancellationToken);
                                                                           }
                                                                           catch
                                                                           {
                                                                               result = false;
                                                                           }
                                                                       }

                                                                       // All done send result back to caller
                                                                       request.OnSendComplete(result);
                                                                   });
        }

        public Task SendAsync<TMessage>(TMessage message, CancellationToken cancellationToken)
        {
            var completionSource = new TaskCompletionSource<bool>();

            _messageProcessor.Post(new SendMessageRequest(message, cancellationToken, result => completionSource.SetResult(result)));

            return completionSource.Task;
        }

        public Guid Subscribe<TMessage>(Func<TMessage, CancellationToken, Task> handler, Action<SubscriptionFactoryOptions<TMessage>> configure)
        {
            var factory = new SubscriptionFactoryOptions<TMessage>();

            configure?.Invoke(factory);

            async Task HandlerWithCheck(object message, CancellationToken cancellationToken)
            {
                if (message is TMessage msg)
                    await handler(msg, cancellationToken);
            }

            var subscription = new Subscription(factory.Id ?? Guid.NewGuid(),
                                                typeof(TMessage),
                                                factory.RunBlindly ?? _defaultRunInBackground,
                                                factory.IncludeDerivedTypes ?? true,
                                                HandlerWithCheck);

            _subscriptionRequests.Enqueue(subscription);

            return subscription.Id;
        }

        public void Unsubscribe(Guid subscriptionId)
        {
            _unsubscribeRequests.Enqueue(subscriptionId);
        }
    }
}