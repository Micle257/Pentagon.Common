// -----------------------------------------------------------------------
//  <copyright file="Subscription.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.EventBus
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    sealed class Subscription
    {
        public Subscription(Guid id,
                            Type messageType,
                            bool runDetached,
                            bool includeDerivedTypes,
                            Func<object, CancellationToken, Task> handler)
        {
            Id = id;
            MessageType = messageType;
            RunDetached = runDetached;
            IncludeDerivedTypes = includeDerivedTypes;
            Handler = handler;
        }

        public Type MessageType { get;  }

        public bool RunDetached { get; }

        public bool IncludeDerivedTypes { get; }

        public Guid Id { get; }

        public Func<object, CancellationToken, Task> Handler { get; }
    }

    public class SubscriptionFactoryOptions<TMessage>
    {
        public bool? RunBlindly { get; set; }

        public Guid? Id { get; set; }

        public bool? IncludeDerivedTypes { get; set; }
    }
}