namespace EShoppingTutorial.Core.Domain;

public interface IUnitOfWork
{
    IOrderRepository OrderRepository { get; }
    Task<int> CompleteAsync();
    Task<int> CompleteAsync(CancellationToken cancellationToken);
}