namespace EShoppingTutorial.Core.Application.Orders.Queries.GetPagedOrders;

public class GetPagedOrdersHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetPagedOrdersQuery, QueryResult<Order>>
{
    public async Task<QueryResult<Order>> Handle(GetPagedOrdersQuery request, CancellationToken cancellationToken)
    {
        return await unitOfWork.OrderRepository
            .GetPageAsync(
                request.QueryObject,
                predicate: null,
                includes: x => x.OrderItems)
            .ConfigureAwait(false);
    }
}