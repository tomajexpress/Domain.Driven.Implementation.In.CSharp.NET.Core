namespace EShoppingTutorial.Core.Application.Orders.Queries.GetPagedOrders;

public record GetPagedOrdersQuery(QueryObjectParams QueryObject) : IRequest<QueryResult<Order>>;
