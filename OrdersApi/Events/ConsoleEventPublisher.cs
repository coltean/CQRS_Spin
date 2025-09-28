namespace OrdersApi.Events
{
    public class ConsoleEventPublisher : IEventPublisher
    {
        public Task PublishAsync<TEvent>(TEvent evt, CancellationToken cancellationToken = default) where TEvent : class
        {
            Console.WriteLine($"Event Published: {typeof(TEvent).Name} - Data: {System.Text.Json.JsonSerializer.Serialize(evt)}");
            return Task.CompletedTask;
        }
    }
}
