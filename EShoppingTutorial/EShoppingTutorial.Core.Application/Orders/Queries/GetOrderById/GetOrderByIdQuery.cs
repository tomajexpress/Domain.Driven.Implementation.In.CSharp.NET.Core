namespace EShoppingTutorial.Core.Application.Orders.Queries.GetOrderById;

public record GetOrderByIdQuery(OrderId Id) : IRequest<Order?>;
