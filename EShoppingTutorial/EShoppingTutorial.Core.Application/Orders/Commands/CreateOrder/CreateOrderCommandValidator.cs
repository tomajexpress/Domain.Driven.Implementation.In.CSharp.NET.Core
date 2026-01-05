namespace EShoppingTutorial.Core.Application.Orders.Commands.CreateOrder;

public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderCommandValidator()
    {
        RuleFor(x => x.CustomerId)
            .GreaterThan(0).WithMessage("Please enter a valid customer id!");

        // Address Component Validations
        RuleFor(x => x.Street)
            .NotEmpty().WithMessage("Street address is required.")
            .NotEqual("string")
            .MaximumLength(100);

        RuleFor(x => x.City)
            .NotEmpty().WithMessage("City is required.")
            .NotEqual("string")
            .MaximumLength(50);

        RuleFor(x => x.Country)
            .NotEmpty().WithMessage("Country is required.")
            .NotEqual("string")
            .MaximumLength(50);

        RuleFor(x => x.ZipCode)
            .NotEmpty().WithMessage("Zip Code is required.")
            .NotEqual("string");

        // Order Items Validation
        RuleFor(x => x.Items)
            .NotEmpty().WithMessage("Please enter order items!");

        RuleForEach(x => x.Items).SetValidator(new OrderItemDtoValidator());
    }
}

public class OrderItemDtoValidator : AbstractValidator<OrderItemDto>
{
    public OrderItemDtoValidator()
    {
        RuleFor(x => x.ProductId)
            .GreaterThan(0).WithMessage("Please enter a valid product");

        RuleFor(x => x.Value)
            .GreaterThan(0).WithMessage("Please enter a valid money value!");

        RuleFor(x => x.Currency)
            .NotEmpty().WithMessage("Please enter a money currency code")
            .IsEnumName(typeof(Currency), caseSensitive: false)
            .WithMessage("Please enter a valid currency code!");
    }
}