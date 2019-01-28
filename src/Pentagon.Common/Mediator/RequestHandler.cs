// -----------------------------------------------------------------------
//  <copyright file="RequestHandler.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Mediator
{
    using System.Threading;
    using System.Threading.Tasks;

    public abstract class RequestHandler<TRequest, TResponse> : IRequestHandler<TRequest, TResponse>
            where TRequest : IRequest<TResponse>
    {
        /// <inheritdoc />
        public abstract Task<TResponse> ExecuteAsync(TRequest request, CancellationToken cancellationToken);

        /// <inheritdoc />
        public TResponse Execute(TRequest request) => ExecuteAsync(request, CancellationToken.None).Result;
    }
}