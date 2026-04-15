namespace EShoppingTutorial.Core.Application.Products.Commands;

public record CreateProductCommand(
    string Name,
    string Description,
    decimal PriceValue,
    string Currency) : IRequest<int>;
