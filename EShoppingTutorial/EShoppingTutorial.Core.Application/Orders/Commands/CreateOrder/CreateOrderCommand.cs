namespace EShoppingTutorial.Core.Application.Orders.Commands.CreateOrder;

public record CreateOrderCommand(Order Order) : IRequest;
