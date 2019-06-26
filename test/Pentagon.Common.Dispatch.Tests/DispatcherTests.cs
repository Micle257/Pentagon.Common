// -----------------------------------------------------------------------
//  <copyright file="DtoPropertyTests.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Common.Tests.Dispatcher
{
    using Dispatch;
    using Microsoft.Extensions.DependencyInjection;
    using Xunit;

    public class DispatcherTests
    {
        [Fact]
        public void AddDispatcher_ArgumentValueTrue_AddsHandlerToProvider()
        {
            var ser = new ServiceCollection()
                    .AddTransient<ICommandHandler<Command1, Response1>, CommandHandler1>();

            var di = ser.BuildServiceProvider();

            var service = new Dispatcher(di);

            var response1 = service.Execute<Command1, Response1>(new Command1()).Result;
        }
    }
}