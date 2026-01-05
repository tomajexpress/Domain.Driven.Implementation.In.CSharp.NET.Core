namespace EShoppingTutorial.Core.Application.Orders.Commands.CreateOrder;

public record CreateOrderCommand(
    int CustomerId,
    string Street,
    string City,
    string Country,
    string ZipCode,
    List<OrderItemDto> Items) : IRequest<int>;

public record OrderItemDto(
    int ProductId,
    decimal Value,
    string Currency);