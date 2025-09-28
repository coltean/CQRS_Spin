using OrdersApi.Data;
using OrdersApi.DTOs;
using OrdersApi.Handlers.Interfaces;
using OrdersApi.Queries;

namespace OrdersApi.Handlers
{
    public class GetOrderByIdQueryHandler : IQueryHandler<GetOrderByIdQuery, OrderDTO?>
    {
        private readonly ReadAppDbContext _readAppDbContext;

        public GetOrderByIdQueryHandler(ReadAppDbContext appDbContext)
        {
            _readAppDbContext = appDbContext;
        }

        public async Task<OrderDTO?> HandleAsync(GetOrderByIdQuery query, CancellationToken cancellationToken = default)
        {
            var order = await _readAppDbContext.Orders.FindAsync([query.Id], cancellationToken);
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
