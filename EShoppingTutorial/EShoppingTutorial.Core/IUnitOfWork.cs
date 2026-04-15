namespace EShoppingTutorial.Core.Domain;

public interface IUnitOfWork
{
    // The interface exposes the Repository contract, but not the internal implementation.
    IOrderRepository OrderRepository { get; }

    IProductRepository ProductRepository { get; }

    Task<int> CompleteAsync();
    Task<int> CompleteAsync(CancellationToken cancellationToken);
}