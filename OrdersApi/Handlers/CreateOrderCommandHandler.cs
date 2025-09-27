using Microsoft.EntityFrameworkCore;
using OrdersApi.Commands;
using OrdersApi.Data;
using OrdersApi.DTOs;
using OrdersApi.Handlers.Interfaces;
using OrdersApi.Models;

namespace OrdersApi.Handlers
{
    public class CreateOrderCommandHandler : ICommandHandler<CreateOrderCommand, OrderDTO>
    {
        private readonly AppDbContext _appDbContext;

        public CreateOrderCommandHandler(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<OrderDTO> HandleAsync(CreateOrderCommand command, CancellationToken cancellationToken = default)
        {
            var order = new Order
            {
                FirstName = command.FirstName,
                LastName = command.LastName,
                Status = command.Status,
                CreatedAt = DateTime.UtcNow,
                TotalCost = command.TotalCost
            };
            await _appDbContext.Orders.AddAsync(order);
            await _appDbContext.SaveChangesAsync(cancellationToken);
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
