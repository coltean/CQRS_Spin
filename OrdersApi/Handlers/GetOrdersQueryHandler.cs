using Microsoft.EntityFrameworkCore;
using OrdersApi.Data;
using OrdersApi.DTOs;
using OrdersApi.Handlers.Interfaces;
using OrdersApi.Models;

namespace OrdersApi.Handlers
{
    public class GetOrdersQueryHandler : IQueryHandler<IEnumerable<OrderDTO>>
    {
        private readonly AppDbContext _appDbContext;

        public GetOrdersQueryHandler(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<IEnumerable<OrderDTO>> HandleAsync(CancellationToken cancellationToken = default)
        {
            var orders = await _appDbContext.Orders.ToListAsync(cancellationToken);
            return orders.Select(order => new OrderDTO(
                order.Id,
                order.FirstName,
                order.LastName,
                order.Status,
                order.CreatedAt,
                order.TotalCost
            ));
        }
    }
}
