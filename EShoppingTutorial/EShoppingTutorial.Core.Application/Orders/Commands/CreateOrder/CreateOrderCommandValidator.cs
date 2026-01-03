namespace EShoppingTutorial.Core.Application.Orders.Commands.CreateOrder;

public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderCommandValidator()
    {
        RuleFor(x => x.ShippingAddress)
            .NotEmpty().WithMessage("You must supply Shipping Address!")
            .NotEqual("string")
            .Length(2, 100);

        RuleFor(x => x.CustomerId)
            .GreaterThan(0).WithMessage("Please enter a valid customer id!");

        RuleFor(x => x.Items)
            .NotEmpty().WithMessage("Please enter order items!");

        // Validate child items using a collection rule
        RuleForEach(x => x.Items).SetValidator(new OrderItemDtoValidator());
    }
}

public class OrderItemDtoValidator : AbstractValidator<OrderItemDto>
{
    public OrderItemDtoValidator()
    {
        RuleFor(x => x.ProductId)
            .GreaterThan(0).WithMessage("Please enter a valid product");

        RuleFor(x => x.Amount)
            .GreaterThan(0).WithMessage("Please enter a valid amount!");

        RuleFor(x => x.Currency)
            .NotEmpty().WithMessage("Please enter a currency unit")
            .IsEnumName(typeof(MoneyUnit), caseSensitive: false)
            .WithMessage("Please enter a valid money unit!");
    }
}