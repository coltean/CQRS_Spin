using Microsoft.EntityFrameworkCore;
using OrdersApi.Data;
using OrdersApi.DTOs;
using OrdersApi.Handlers.Interfaces;

namespace OrdersApi.Handlers
{
    public class GetOrdersQueryHandler : IQueryHandler<IEnumerable<OrderDTO>>
    {
        private readonly ReadAppDbContext _readAppDbContext;

        public GetOrdersQueryHandler(ReadAppDbContext readAppDbContext)
        {
            _readAppDbContext = readAppDbContext;
        }

        public async Task<IEnumerable<OrderDTO>> HandleAsync(CancellationToken cancellationToken = default)
        {
            var orders = await _readAppDbContext.Orders.ToListAsync(cancellationToken);
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
