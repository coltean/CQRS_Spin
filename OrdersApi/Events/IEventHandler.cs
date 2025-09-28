namespace OrdersApi.Events
{
    public interface IEventHandler<TEvent> where TEvent : class
    {
        Task HandleAsync(TEvent evt, CancellationToken cancellationToken = default);
    }
}
