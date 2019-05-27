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

    public class InProcessBus : IEventBus
    {
        readonly ConcurrentQueue<Subscription> _subscriptionRequests = new ConcurrentQueue<Subscription>();

        readonly ConcurrentQueue<Guid> _unsubscribeRequests = new ConcurrentQueue<Guid>();

        readonly ActionBlock<SendMessageRequest> _messageProcessor;
        
        public InProcessBus()
        {
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

        public Task SendAsync<TMessage>(TMessage message) => SendAsync(message, CancellationToken.None);

        public Task SendAsync<TMessage>(TMessage message, CancellationToken cancellationToken)
        {
            var completionSource = new TaskCompletionSource<bool>();
            _messageProcessor.Post(new SendMessageRequest(message, cancellationToken, result => completionSource.SetResult(result)));
            return completionSource.Task;
        }

        public Guid Subscribe<TMessage>(Action<TMessage> handler, Guid? id)
        {
            return Subscribe<TMessage>((message, cancellationToken) =>
                                       {
                                           handler(message);
                                           return Task.FromResult(0);
                                       }, id);
        }

        public Guid Subscribe<TMessage>(Func<TMessage, CancellationToken, Task> handler, Guid? id)
        {
            var subscription = id.HasValue ? Subscription.Create(id.Value, handler) : Subscription.Create(handler);
            _subscriptionRequests.Enqueue(subscription);
            return subscription.Id;
        }

        public void Unsubscribe(Guid subscriptionId)
        {
            _unsubscribeRequests.Enqueue(subscriptionId);
        }
    }
}