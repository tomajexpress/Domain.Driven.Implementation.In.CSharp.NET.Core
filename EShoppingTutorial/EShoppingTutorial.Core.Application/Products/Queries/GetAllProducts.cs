namespace EShoppingTutorial.Core.Application.Products.Queries;

public record GetAllProductsQuery() : IRequest<QueryResult<ProductViewModel>>;

internal class GetAllProductsHandler(IUnitOfWork unitOfWork, IMapper mapper) 
    : IRequestHandler<GetAllProductsQuery, QueryResult<ProductViewModel>>
{
    public async Task<QueryResult<ProductViewModel>> Handle(GetAllProductsQuery request, CancellationToken ct)
    {
        var products = await unitOfWork.ProductRepository
            .GetAllAsync()
            .ConfigureAwait(false);

        var mappedItems = mapper.Map<IEnumerable<ProductViewModel>>(products);

        return new QueryResult<ProductViewModel>(mappedItems, mappedItems.Count());
    }
}