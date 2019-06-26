// -----------------------------------------------------------------------
//  <copyright file="IRequest'1.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Dispatch
{
    public interface ICommand<out TResponse> : ICommand { }
}