// -----------------------------------------------------------------------
//  <copyright file="CommandHandlerDelegate.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Dispatch
{
    using System.Threading.Tasks;

    public delegate Task<TResponse> CommandHandlerDelegate<TResponse>();
}