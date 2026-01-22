namespace EShoppingTutorial.Core.Application.Orders.Queries.GetAllOrders;

internal class GetAllOrdersHandler(IUnitOfWork unitOfWork, IMapper mapper)
    : IRequestHandler<GetAllOrdersQuery, QueryResult<OrderViewModel>>
{
    public async Task<QueryResult<OrderViewModel>> Handle(GetAllOrdersQuery request, CancellationToken ct)
    {
        var orders = await unitOfWork.OrderRepository
            .GetAllAsync(x => x.OrderItems)
            .ConfigureAwait(false);

        var mappedItems = mapper.Map<IEnumerable<OrderViewModel>>(orders);

        return new QueryResult<OrderViewModel>(mappedItems, mappedItems.Count());
    }
}