// -----------------------------------------------------------------------
//  <copyright file="CommandHandlerWrapper.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Dispatch
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using JetBrains.Annotations;
    using Microsoft.Extensions.DependencyInjection;

    class CommandHandlerWrapper<TRequest, TResponse>
            where TRequest : ICommand<TResponse>
    {
        public Task<TResponse> Handle(ICommand<TResponse> command,
                                      CancellationToken cancellationToken,
                                      IServiceProvider serviceFactory)
        {
            var handler = GetHandler<ICommandHandler<TRequest, TResponse>>(serviceFactory);

            Task<TResponse> Handler() => handler.ExecuteAsync((TRequest) command, cancellationToken);

            return serviceFactory
                   .GetServices<IPipelineBehavior<TRequest, TResponse>>()
                   .Reverse()
                   .Aggregate((CommandHandlerDelegate<TResponse>) Handler, (next, pipeline) => () => pipeline.Handle((TRequest) command, cancellationToken, next))();
        }

        static THandler GetHandler<THandler>([NotNull] IServiceProvider services)
                where THandler : class
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            THandler handler;

            try
            {
                handler = (THandler) services.GetService(typeof(THandler));
            }
            catch (Exception e)
            {
                throw new InvalidOperationException($"Error constructing handler for request of type {typeof(THandler)}. Register your handlers with the container. See the samples in GitHub for examples.", e);
            }

            if (handler == null)
                throw new InvalidOperationException($"Handler was not found for request of type {typeof(THandler)}. Register your handlers with the container. See the samples in GitHub for examples.");

            return handler;
        }
    }
}