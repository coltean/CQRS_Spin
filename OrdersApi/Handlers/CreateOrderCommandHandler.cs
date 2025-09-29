using FluentValidation;
using MediatR;
using OrdersApi.Commands;
using OrdersApi.Data;
using OrdersApi.DTOs;
using OrdersApi.Events;
using OrdersApi.Models;

namespace OrdersApi.Handlers
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, OrderDTO>
    {
        private readonly WriteAppDbContext _writeAppDbContext;
        private readonly IValidator<CreateOrderCommand> _validator;
        private readonly IMediator _mediator;

        public CreateOrderCommandHandler(WriteAppDbContext writeAppDbContext, IValidator<CreateOrderCommand> validator, IMediator mediator)
        {
            _writeAppDbContext = writeAppDbContext;
            _validator = validator;
            _mediator = mediator;
        }

        public async Task<OrderDTO> Handle(CreateOrderCommand command, CancellationToken cancellationToken)
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
            await _writeAppDbContext.Orders.AddAsync(order);
            await _writeAppDbContext.SaveChangesAsync(cancellationToken);
            await _mediator.Publish(new OrderCreatedEvent(order.Id, order.FirstName, order.LastName, order.Status, order.TotalCost), cancellationToken);
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
