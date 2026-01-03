namespace EShoppingTutorial.Core.Application.Orders.Queries.GetOrderById;

public record GetOrderByIdQuery(int Id) : IRequest<OrderViewModel?>;