// -----------------------------------------------------------------------
//  <copyright file="IRequest'1.cs">
//   Copyright (c) Michal Pokorn�. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Mediator
{
    public interface IRequest<out TResponse> : IRequest { }
}