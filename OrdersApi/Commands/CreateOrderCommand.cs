using MediatR;
using OrdersApi.DTOs;

namespace OrdersApi.Commands
{
    public record CreateOrderCommand(string FirstName, string LastName, string Status, decimal TotalCost) : IRequest<OrderDTO>
    {
    }
}
