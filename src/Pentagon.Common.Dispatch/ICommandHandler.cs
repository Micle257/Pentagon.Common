// -----------------------------------------------------------------------
//  <copyright file="IRequestHandler.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Dispatch
{
    using System.Threading;
    using System.Threading.Tasks;

    public interface ICommandHandler<in TRequest, TResponse>
            where TRequest : ICommand<TResponse>
    {
        Task<TResponse> ExecuteAsync(TRequest request, CancellationToken cancellationToken);

        TResponse Execute(TRequest request);
    }
}