// -----------------------------------------------------------------------
//  <copyright file="Mediator.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Mediator
{
    using System;
    using System.Collections.Concurrent;
    using System.Threading;
    using System.Threading.Tasks;

    public class Mediator : IMediator
    {
        static readonly ConcurrentDictionary<Type, object> _requestHandlers = new ConcurrentDictionary<Type, object>();
        readonly IServiceProvider _serviceFactory;

        /// <summary> Initializes a new instance of the <see cref="Mediator" /> class. </summary>
        /// <param name="serviceFactory"> The single instance factory. </param>
        public Mediator(IServiceProvider serviceFactory)
        {
            _serviceFactory = serviceFactory;
        }

        public Task<TResponse> Execute<TRequest, TResponse>(TRequest request, CancellationToken cancellationToken = default)
                where TRequest : IRequest<TResponse>
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            var requestType = request.GetType();

            var handler = (RequestHandlerWrapper<TRequest, TResponse>) _requestHandlers.GetOrAdd(requestType,
                                                                                                 t => Activator.CreateInstance(typeof(RequestHandlerWrapper<,>).MakeGenericType(requestType, typeof(TResponse))));

            return handler.Handle(request, cancellationToken, _serviceFactory);
        }
    }
}