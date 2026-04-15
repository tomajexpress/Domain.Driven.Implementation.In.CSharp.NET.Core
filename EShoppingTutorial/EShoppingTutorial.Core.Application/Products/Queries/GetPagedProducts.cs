namespace EShoppingTutorial.Core.Application.Products.Queries;

public record GetPagedProductsQuery(QueryObjectParams QueryObject) : IRequest<QueryResult<ProductViewModel>>;

internal class GetPagedProductsHandler(IUnitOfWork unitOfWork, IMapper mapper) 
    : IRequestHandler<GetPagedProductsQuery, QueryResult<ProductViewModel>>
{
    public async Task<QueryResult<ProductViewModel>> Handle(GetPagedProductsQuery request, CancellationToken ct)
    {
        var pagedResult = await unitOfWork.ProductRepository
            .GetPageAsync(request.QueryObject)
            .ConfigureAwait(false);

        var mappedViewModels = mapper.Map<IEnumerable<ProductViewModel>>(pagedResult.Entities);

        return new QueryResult<ProductViewModel>(mappedViewModels, pagedResult.TotalCount);
    }
}