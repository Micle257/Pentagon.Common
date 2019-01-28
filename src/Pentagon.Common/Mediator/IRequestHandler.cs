// -----------------------------------------------------------------------
//  <copyright file="IRequestHandler.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Mediator
{
    using System.Threading;
    using System.Threading.Tasks;

    public interface IRequestHandler<in TRequest, TResponse>
            where TRequest : IRequest<TResponse>
    {
        Task<TResponse> ExecuteAsync(TRequest request, CancellationToken cancellationToken);

        TResponse Execute(TRequest request);
    }
}