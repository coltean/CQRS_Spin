using MediatR;
using OrdersApi.Data;
using OrdersApi.Events;

namespace OrdersApi.Projections
{
    public class OrderCreatedProjectionHandler : INotificationHandler<OrderCreatedEvent>
    {
        private readonly ReadAppDbContext _readAppDbContext;

        public OrderCreatedProjectionHandler(ReadAppDbContext readAppDbContext)
        {
            this._readAppDbContext = readAppDbContext;
        }

        public async Task Handle(OrderCreatedEvent notification, CancellationToken cancellationToken)
        {
            var order = new Models.Order
            {
                Id = notification.OrderId,
                FirstName = notification.FirstName,
                LastName = notification.LastName,
                Status = notification.Status,
                CreatedAt = DateTime.UtcNow,
                TotalCost = notification.TotalCost
            };

            await _readAppDbContext.Orders.AddAsync(order, cancellationToken);
            await _readAppDbContext.SaveChangesAsync(cancellationToken);
            Console.WriteLine($"OrderCreatedProjectionHandler: Projected order with ID {order.Id} to read database.");
        }
    }
}
