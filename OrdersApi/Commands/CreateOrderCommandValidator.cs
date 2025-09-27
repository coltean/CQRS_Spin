using FluentValidation;

namespace OrdersApi.Commands
{
    public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
    {
        public CreateOrderCommandValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("First name is required.")
                .MaximumLength(50).WithMessage("First name cannot exceed 50 characters.");
            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Last name is required.")
                .MaximumLength(50).WithMessage("Last name cannot exceed 50 characters.");
            RuleFor(x => x.TotalCost)
                .GreaterThan(0).WithMessage("Total cost must be greater than zero.");
            RuleFor(x => x.Status).NotEmpty().WithMessage("Status is required.");
        }
    }
}
