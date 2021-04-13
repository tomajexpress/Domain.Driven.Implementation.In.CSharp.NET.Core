using FluentValidation;

namespace EShoppingTutorialWebAPI.Models.OrderModels
{
    public class PriceSaveRequestModelValidator : AbstractValidator<PriceSaveRequestModel>
    {
        public PriceSaveRequestModelValidator()
        {
            RuleFor(x => x.Amount)
                .NotNull();

            RuleFor(x => x.Unit)
                .NotNull()
                .IsInEnum();
        }
    }
}
