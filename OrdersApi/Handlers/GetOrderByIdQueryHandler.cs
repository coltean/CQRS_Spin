using MediatR;
using OrdersApi.Data;
using OrdersApi.DTOs;
using OrdersApi.Queries;

namespace OrdersApi.Handlers
{
    public class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQuery, OrderDTO?>
    {
        private readonly ReadAppDbContext _readAppDbContext;

        public GetOrderByIdQueryHandler(ReadAppDbContext appDbContext)
        {
            _readAppDbContext = appDbContext;
        }

        public async Task<OrderDTO?> Handle(GetOrderByIdQuery query, CancellationToken cancellationToken)
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
