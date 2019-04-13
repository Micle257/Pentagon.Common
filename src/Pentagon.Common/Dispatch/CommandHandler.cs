// -----------------------------------------------------------------------
//  <copyright file="RequestHandler.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Dispatch
{
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Represents a handler for command-request process.
    /// </summary>
    /// <typeparam name="TRequest">The type of the request.</typeparam>
    /// <typeparam name="TResponse">The type of the response.</typeparam>
    public abstract class CommandHandler<TRequest, TResponse> : ICommandHandler<TRequest, TResponse>
            where TRequest : ICommand<TResponse>
    {
        /// <inheritdoc />
        public abstract Task<TResponse> ExecuteAsync(TRequest request, CancellationToken cancellationToken);

        /// <inheritdoc />
        public TResponse Execute(TRequest request) => ExecuteAsync(request, CancellationToken.None).Result;
    }
}