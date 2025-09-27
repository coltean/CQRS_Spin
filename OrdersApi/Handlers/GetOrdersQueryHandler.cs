using Microsoft.EntityFrameworkCore;
using OrdersApi.Data;
using OrdersApi.Models;
using OrdersApi.Queries;

namespace OrdersApi.Handlers
{
    public class GetOrdersQueryHandler
    {
        public static async Task<IEnumerable<Order>> HandleAsync(GetOrdersQuery query, AppDbContext dbContext, CancellationToken cancellationToken = default)
        {
            return await dbContext.Orders.ToListAsync(cancellationToken);
        }
    }
}
