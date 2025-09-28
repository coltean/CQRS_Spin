namespace OrdersApi.Events
{
    public interface IEventPublisher
    {
        Task PublishAsync<TEvent>(TEvent evt, CancellationToken cancellationToken = default) where TEvent : class;
    }
}
