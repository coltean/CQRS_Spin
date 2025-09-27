using OrdersApi.Commands;
using OrdersApi.Data;
using OrdersApi.Models;

namespace OrdersApi.Handlers
{
    public class CreateOrderCommandHandler
    {
        public static async Task<Order> HandleAsync(CreateOrderCommand command, AppDbContext dbContext, CancellationToken cancellationToken = default)
        {
            var order = new Order
            {
                FirstName = command.FirstName,
                LastName = command.LastName,
                Status = command.Status,
                CreatedAt = DateTime.UtcNow,
                TotalCost = command.TotalCost
            };
            dbContext.Orders.Add(order);
            await dbContext.SaveChangesAsync(cancellationToken);
            return order;
        }
    }
}
