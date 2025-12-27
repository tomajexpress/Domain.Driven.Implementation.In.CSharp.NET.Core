namespace EShoppingTutorial.Core.Application.Orders.Commands.DeleteOrder;

public record DeleteOrderCommand(OrderId Id) : IRequest<bool>;
