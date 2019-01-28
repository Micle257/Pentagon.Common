// -----------------------------------------------------------------------
//  <copyright file="IPipelineBehavior.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Mediator
{
    using System.Threading;
    using System.Threading.Tasks;

    public interface IPipelineBehavior<in TRequest, TResponse>
    {
        Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next);
    }
}