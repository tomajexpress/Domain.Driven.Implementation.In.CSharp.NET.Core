namespace EShoppingTutorialWebAPI.Models.OrderModels;

public class OrderSaveRequestModelValidator : AbstractValidator<OrderSaveRequestModel>
{
    public OrderSaveRequestModelValidator()
    {
        RuleFor(x => x.ShippingAdress)
       .NotNull()
       .NotEmpty()
       .NotEqual(string.Empty)
       .NotEqual("string")
       .Length(2, 100);

        RuleFor(x => x.CustomerId)
       .NotNull()
       .GreaterThan(0).WithMessage("Please enter a valid customer id!");

        RuleFor(x => x.OrderItemsDtoModel)
       .NotNull().WithMessage("Please enter order items!");
    }
}
