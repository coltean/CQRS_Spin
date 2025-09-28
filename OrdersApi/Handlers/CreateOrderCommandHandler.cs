using FluentValidation;
using Microsoft.EntityFrameworkCore;
using OrdersApi.Commands;
using OrdersApi.Data;
using OrdersApi.DTOs;
using OrdersApi.Events;
using OrdersApi.Handlers.Interfaces;
using OrdersApi.Models;

namespace OrdersApi.Handlers
{
    public class CreateOrderCommandHandler : ICommandHandler<CreateOrderCommand, OrderDTO>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IValidator<CreateOrderCommand> _validator;
        private readonly IEventPublisher _eventPublisher;

        public CreateOrderCommandHandler(AppDbContext appDbContext, IValidator<CreateOrderCommand> validator, IEventPublisher eventPublisher)
        {
            _appDbContext = appDbContext;
            _validator = validator;
            _eventPublisher = eventPublisher;
        }

        public async Task<OrderDTO> HandleAsync(CreateOrderCommand command, CancellationToken cancellationToken = default)
        {
            var validationResult = await _validator.ValidateAsync(command, cancellationToken);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

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
            await _eventPublisher.PublishAsync(new OrderCreatedEvent(order.Id, order.FirstName, order.LastName, order.Status, order.TotalCost), cancellationToken);
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
