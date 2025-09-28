
namespace OrdersApi.Events
{
    public class InProcessEventPublisher : IEventPublisher
    {
        private readonly IServiceProvider _serviceProvider;

        public InProcessEventPublisher(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task PublishAsync<TEvent>(TEvent evt, CancellationToken cancellationToken = default) where TEvent : class
        {
            using var scope = _serviceProvider.CreateScope();
            var handlers = scope.ServiceProvider.GetServices<IEventHandler<TEvent>>();

            foreach (var handler in handlers)
            {
                await handler.HandleAsync(evt, cancellationToken);
            }
        }
    }
}
