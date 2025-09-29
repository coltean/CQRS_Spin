using MediatR;
using OrdersApi.DTOs;

namespace OrdersApi.Queries
{
    public class GetOrdersQuery : IRequest<IEnumerable<OrderDTO>> { }

}
