using FluentValidation;

namespace EShoppingTutorialWebAPI.Models.OrderModels
{
    public class PriceSaveRequestModelValidator : AbstractValidator<PriceSaveRequestModel>
    {
        public PriceSaveRequestModelValidator()
        {
            RuleFor(x => x.Amount)
            .NotNull()
            .GreaterThan(0).WithMessage("Please enter a valid amount!");

            RuleFor(x => x.Unit)
            .NotNull()
            .IsInEnum()
            .Must(unit => unit != EShoppingTutorial.Core.Domain.Enums.MoneyUnit.UnSpecified)
            .WithMessage("Please enter a valid money unit!");
        }
    }
}
