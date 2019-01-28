// -----------------------------------------------------------------------
//  <copyright file="IRequest'1.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Mediator
{
    public interface IRequest<out TResponse> : IRequest { }
}