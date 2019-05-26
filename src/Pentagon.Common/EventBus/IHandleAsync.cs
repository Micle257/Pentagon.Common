// -----------------------------------------------------------------------
//  <copyright file="IHandleAsync.cs">
//   Copyright (c) Smartdata s. r. o. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.EventBus
{
    using System.Threading;
    using System.Threading.Tasks;

    public interface IHandleAsync<TMessage>
    {
        Task HandleAsync(TMessage message, CancellationToken cancellationToken);
    }
}