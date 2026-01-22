namespace EShoppingTutorial.Core.Application.Orders.Queries.GetPagedOrders;

internal class GetPagedOrdersHandler(IUnitOfWork unitOfWork, IMapper mapper)
    : IRequestHandler<GetPagedOrdersQuery, QueryResult<OrderViewModel>>
{
    public async Task<QueryResult<OrderViewModel>> Handle(GetPagedOrdersQuery request, CancellationToken ct)
    {
        var pagedResult = await unitOfWork.OrderRepository
            .GetPageAsync(
                request.QueryObject,
                predicate: null,
                includes: x => x.OrderItems)
            .ConfigureAwait(false);

        var mappedViewModels = mapper.Map<IEnumerable<OrderViewModel>>(pagedResult.Entities);

        return new QueryResult<OrderViewModel>(mappedViewModels, pagedResult.TotalCount);
    }
}