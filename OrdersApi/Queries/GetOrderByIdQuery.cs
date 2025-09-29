using MediatR;
using OrdersApi.DTOs;

namespace OrdersApi.Queries
{
    public record GetOrderByIdQuery(int Id) : IRequest<OrderDTO?>
    {

    }
}
