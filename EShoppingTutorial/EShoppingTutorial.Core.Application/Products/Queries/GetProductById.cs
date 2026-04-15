namespace EShoppingTutorial.Core.Application.Products.Queries;

public record GetProductByIdQuery(int Id) : IRequest<ProductViewModel?>;

internal class GetProductByIdHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<GetProductByIdQuery, ProductViewModel?>
{
    public async Task<ProductViewModel?> Handle(GetProductByIdQuery request, CancellationToken ct)
    {
        var product = await unitOfWork.ProductRepository
            .GetAsync(predicate: x => x.Id == new ProductId(request.Id))
            .ConfigureAwait(false);

        if (product == null) return null;

        return mapper.Map<ProductViewModel>(product);
    }
}