namespace EShoppingTutorial.Core.Application.Products.Commands.CreateProduct;

internal class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        // Name Validation
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Product name is required.")
            .NotEqual("string").WithMessage("Please provide a real product name.")
            .MaximumLength(200).WithMessage("Product name must not exceed 200 characters.");

        // Description Validation
        // It is optional, but if provided, it must fit in the database column
        RuleFor(x => x.Description)
            .MaximumLength(500).WithMessage("Description must not exceed 500 characters.");

        // Price Value Validation
        RuleFor(x => x.PriceValue)
            .GreaterThan(0).WithMessage("Please enter a valid, positive money value!");

        // Currency Validation
        RuleFor(x => x.Currency)
            .NotEmpty().WithMessage("Please enter a money currency code.")
            .IsEnumName(typeof(Currency), caseSensitive: false)
            .WithMessage("Please enter a valid currency code (e.g., USD, EUR, Rial)!");
    }
}