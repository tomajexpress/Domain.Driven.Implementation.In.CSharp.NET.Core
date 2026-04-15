namespace EShoppingTutorial.Core.Application.Products.Commands;

internal class CreateProductHandler(IUnitOfWork unitOfWork) : IRequestHandler<CreateProductCommand, int>
{
    public async Task<int> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        // Enum aus dem String parsen
        var currency = Enum.Parse<Currency>(request.Currency, true);

        // Value Object für den Preis erstellen (Validierung passiert automatisch im Price Record)
        var price = new Price(request.PriceValue, currency);

        // Die reiche Domänen-Entität erstellen
        var product = new Product(request.Name, request.Description, price);

        // Über das Unit of Work speichern (Hinweis: IProductRepository muss im UnitOfWork noch ergänzt werden!)
        unitOfWork.ProductRepository.Add(product);

        await unitOfWork.CompleteAsync(cancellationToken).ConfigureAwait(false);

        return product.Id.Value;
    }
}
