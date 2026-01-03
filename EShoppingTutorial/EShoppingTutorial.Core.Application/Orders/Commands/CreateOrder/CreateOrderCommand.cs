namespace EShoppingTutorial.Core.Application.Orders.Commands.CreateOrder;

public record CreateOrderCommand(
    int CustomerId,
    string ShippingAddress,
    List<OrderItemDto> Items) : IRequest<int>;

public record OrderItemDto(
    int ProductId,
    decimal Amount,
    string Currency);