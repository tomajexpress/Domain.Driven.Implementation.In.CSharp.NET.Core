namespace EShoppingTutorial.Core.Application.Products.Queries;

public record ProductViewModel(
    int Id,
    string Name,
    string Description,
    decimal PriceValue,
    string PriceCurrency);