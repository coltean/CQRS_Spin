using OrdersApi.Data;
using OrdersApi.Models;
using OrdersApi.Queries;

namespace OrdersApi.Handlers
{
    public class GetOrderByIdQueryHandler
    {
        public static async Task<Order?> HandleAsync(GetOrderByIdQuery query, AppDbContext dbContext, CancellationToken cancellationToken = default)
        {
            return await dbContext.Orders.FindAsync([query.Id], cancellationToken);
        }
    }
}
