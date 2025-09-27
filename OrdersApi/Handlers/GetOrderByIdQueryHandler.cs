using OrdersApi.Data;
using OrdersApi.DTOs;
using OrdersApi.Handlers.Interfaces;
using OrdersApi.Models;
using OrdersApi.Queries;

namespace OrdersApi.Handlers
{
    public class GetOrderByIdQueryHandler : IQueryHandler<GetOrderByIdQuery, OrderDTO?>
    {
        private readonly AppDbContext _appDbContext;

        public GetOrderByIdQueryHandler(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<OrderDTO?> HandleAsync(GetOrderByIdQuery query, CancellationToken cancellationToken = default)
        {
            var order = await _appDbContext.Orders.FindAsync([query.Id], cancellationToken);
            if (order is null)
            {
                return null;
            }

            return new OrderDTO(
                order.Id,
                order.FirstName,
                order.LastName,
                order.Status,
                order.CreatedAt,
                order.TotalCost
            );
        }
    }
}
