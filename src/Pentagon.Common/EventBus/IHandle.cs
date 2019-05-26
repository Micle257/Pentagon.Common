// -----------------------------------------------------------------------
//  <copyright file="IHandle.cs">
//   Copyright (c) Smartdata s. r. o. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.EventBus
{
    public interface IHandle<TMessage>
    {
        void Handle(TMessage message);
    }
}