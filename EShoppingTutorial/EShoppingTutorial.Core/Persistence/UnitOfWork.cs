namespace EShoppingTutorial.Core.Persistence;

public class UnitOfWork : IUnitOfWork, IAsyncDisposable
{
    private readonly EShoppingTutorialDbContext _context;

    public IOrderRepository OrderRepository { get; private set; }

    public UnitOfWork(EShoppingTutorialDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        OrderRepository = new OrderRepository(_context);
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