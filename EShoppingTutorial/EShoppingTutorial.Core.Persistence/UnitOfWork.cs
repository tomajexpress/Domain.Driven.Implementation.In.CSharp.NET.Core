namespace EShoppingTutorial.Core.Persistence;

public class UnitOfWork : IUnitOfWork, IAsyncDisposable
{
    private readonly EShoppingTutorialDbContext _context;

    // Lazy holders for our repositories
    private readonly Lazy<IOrderRepository> _orderRepository;

    public IOrderRepository OrderRepository => _orderRepository.Value;

    public UnitOfWork(EShoppingTutorialDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));

        // Define how to create the repository, but don't create it yet
        _orderRepository = new Lazy<IOrderRepository>(() => new OrderRepository(_context));
    }

    public async Task<int> CompleteAsync()
    {
        return await _context.SaveChangesAsync().ConfigureAwait(false);
    }

    public async Task<int> CompleteAsync(CancellationToken cancellationToken)
    {
        return await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
    }

    public ValueTask DisposeAsync()
    {
        return _context.DisposeAsync();
    }
}