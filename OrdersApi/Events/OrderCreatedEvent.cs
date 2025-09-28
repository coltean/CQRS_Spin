namespace OrdersApi.Events
{
    public record OrderCreatedEvent(int OrderId, string FirstName, string LastName, string Status, decimal TotalCost)
    {
    }
}
