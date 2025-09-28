using OrdersApi.Data;
using OrdersApi.Events;

namespace OrdersApi.Projections
{
    public class OrderCreatedProjectionHandler : IEventHandler<OrderCreatedEvent>
    {
        private readonly ReadAppDbContext _readAppDbContext;

        public OrderCreatedProjectionHandler(ReadAppDbContext readAppDbContext)
        {
            this._readAppDbContext = readAppDbContext;
        }

        public async Task HandleAsync(OrderCreatedEvent evt, CancellationToken cancellationToken = default)
        {
            var order = new Models.Order
            {
                Id = evt.OrderId,
                FirstName = evt.FirstName,
                LastName = evt.LastName,
                Status = evt.Status,
                CreatedAt = DateTime.UtcNow,
                TotalCost = evt.TotalCost
            };

            await _readAppDbContext.Orders.AddAsync(order, cancellationToken);
            await _readAppDbContext.SaveChangesAsync(cancellationToken);
            Console.WriteLine($"OrderCreatedProjectionHandler: Projected order with ID {order.Id} to read database.");
        }
    }
}
