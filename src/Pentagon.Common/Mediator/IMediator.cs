// -----------------------------------------------------------------------
//  <copyright file="IMediator.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Mediator
{
    using System.Threading;
    using System.Threading.Tasks;

    public interface IMediator
    {
        Task<TResponse> Execute<TRequest, TResponse>(TRequest request, CancellationToken cancellationToken = default)
                where TRequest : IRequest<TResponse>;
    }
}