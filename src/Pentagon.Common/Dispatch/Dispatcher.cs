// -----------------------------------------------------------------------
//  <copyright file="Dispatcher.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Dispatch
{
    using System;
    using System.Collections.Concurrent;
    using System.Threading;
    using System.Threading.Tasks;

    public class Dispatcher : IDispatcher
    {
        static readonly ConcurrentDictionary<Type, object> _requestHandlers = new ConcurrentDictionary<Type, object>();

        readonly IServiceProvider _serviceFactory;

        /// <summary> Initializes a new instance of the <see cref="Dispatcher" /> class. </summary>
        /// <param name="serviceFactory"> The single instance factory. </param>
        public Dispatcher(IServiceProvider serviceFactory)
        {
            _serviceFactory = serviceFactory;
        }

        public Task<TResponse> Execute<TRequest, TResponse>(TRequest request, CancellationToken cancellationToken = default)
                where TRequest : ICommand<TResponse>
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            var requestType = request.GetType();

            var s = typeof(ICommandHandler<,>).MakeGenericType(typeof(TRequest), typeof(TResponse));

            var commandHandler = _serviceFactory.GetService(s);

            if (commandHandler == null)
            {
                throw new ArgumentException("Command handler is not registered in service provider.");
            }

            var commandHandler1 = (ICommandHandler<TRequest, TResponse>)commandHandler;

            return commandHandler1.ExecuteAsync(request, cancellationToken);

            // var handler = (CommandHandlerWrapper<TRequest, TResponse>) _requestHandlers.GetOrAdd(requestType,
            //                                                                                      t => Activator.CreateInstance(typeof(CommandHandlerWrapper<,>).MakeGenericType(requestType, typeof(TResponse))));
            //
            // return handler.Handle(request, cancellationToken, _serviceFactory);
        }
    }
}