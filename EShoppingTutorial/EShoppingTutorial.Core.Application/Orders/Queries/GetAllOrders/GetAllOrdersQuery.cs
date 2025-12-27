namespace EShoppingTutorial.Core.Application.Orders.Queries.GetAllOrders;

public record GetAllOrdersQuery() : IRequest<IEnumerable<Order>>;
