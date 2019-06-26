namespace Pentagon.Common.Tests.Dispatcher {
    using Dispatch;
    using Microsoft.Extensions.DependencyInjection;
    using Xunit;

    public class DispatcherServiceCollectionExtensionsTests
    {
        [Fact]
        public void AddDispatcher_ArgumentValueTrue_AddsHandlerToProvider()
        {
            var ser = new ServiceCollection();

            ser.AddDispatcher(true);

            var di = ser.BuildServiceProvider();

            var handler = di.GetService<ICommandHandler<Command1, Response1>>();

            Assert.NotNull(handler);
        }

        [Fact]
        public void AddDispatcher_ArgumentValueFalse_WontAddHandlerToProvider()
        {
            var ser = new ServiceCollection();

            ser.AddDispatcher(false);

            var di = ser.BuildServiceProvider();

            var handler = di.GetService<ICommandHandler<Command1, Response1>>();

            Assert.Null(handler);
        }
    }
}