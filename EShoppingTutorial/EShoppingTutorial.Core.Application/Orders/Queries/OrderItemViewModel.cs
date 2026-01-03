using EShoppingTutorial.Core.Application.Orders.Queries;
public record OrderItemViewModel(
    int Id,
    int ProductId,
    PriceDto Price);