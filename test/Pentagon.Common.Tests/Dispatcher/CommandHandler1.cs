namespace Pentagon.Common.Tests.Dispatcher {
    using System.Threading;
    using System.Threading.Tasks;
    using Dispatch;

    public class CommandHandler1 : CommandHandler<Command1, Response1>
    {
        /// <inheritdoc />
        public override Task<Response1> ExecuteAsync(Command1 request, CancellationToken cancellationToken)
        {
            return Task.FromResult(new Response1());
        }
    }
}