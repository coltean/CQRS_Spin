using Microsoft.EntityFrameworkCore;
using OrdersApi.Data;
using OrdersApi.Models;

namespace OrdersApi.Handlers
{
    public class GetOrdersQueryHandler
    {
        public static async Task<IEnumerable<Order>> HandleAsync(AppDbContext dbContext, CancellationToken cancellationToken = default)
        {
            return await dbContext.Orders.ToListAsync(cancellationToken);
        }
    }
}
