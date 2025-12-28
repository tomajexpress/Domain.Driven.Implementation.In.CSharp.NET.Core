namespace EShoppingTutorialWebAPI.Models.OrderModels;

public class OrderItemSaveRequestModelValidator : AbstractValidator<OrderItemSaveRequestModel>
{
    public OrderItemSaveRequestModelValidator()
    {
        RuleFor(x => x.ProductId)
        .NotNull()
        .GreaterThan(0)
        .WithMessage("Please enter a product");

        RuleFor(x => x.Price)
        .NotNull().WithMessage("Please enter price");            
    }
}
