// -----------------------------------------------------------------------
//  <copyright file="RequestHandlerDelegate.cs">
//   Copyright (c) Michal Pokorn�. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Mediator
{
    using System.Threading.Tasks;

    public delegate Task<TResponse> RequestHandlerDelegate<TResponse>();
}