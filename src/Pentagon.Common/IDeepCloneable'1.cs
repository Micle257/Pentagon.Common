// -----------------------------------------------------------------------
//  <copyright file="IDeepCloneable'1.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon
{
    public interface IDeepCloneable<out T> : IDeepCloneable
    {
        T DeepClone();
    }
}