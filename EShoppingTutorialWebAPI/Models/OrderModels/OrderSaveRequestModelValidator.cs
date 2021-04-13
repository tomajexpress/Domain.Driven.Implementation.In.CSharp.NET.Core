using FluentValidation;

namespace EShoppingTutorialWebAPI.Models.OrderModels
{
    public class OrderSaveRequestModelValidator : AbstractValidator<OrderSaveRequestModel>
    {
        public OrderSaveRequestModelValidator()
        {
            RuleFor(x => x.ShippingAdress)
           .NotNull()
           .NotEmpty()
           .Length(2, 100);

            RuleFor(x => x.OrderItemsDtoModel)
           .NotNull().WithMessage("Please enter order items!");
        }
    }
}
