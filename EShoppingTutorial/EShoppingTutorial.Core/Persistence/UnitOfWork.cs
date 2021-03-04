using System;
using System.Threading;
using System.Threading.Tasks;
using EShoppingTutorial.Core.Domain;
using EShoppingTutorial.Core.Domain.Repositories;
using EShoppingTutorial.Core.Persistence.Repositories;

namespace EShoppingTutorial.Core.Persistence
{
    public class UnitOfWork : IUnitOfWork, IAsyncDisposable
    {
        private readonly EShoppingTutorialDbContext _context;

        public IOrderRepository OrderRepository { get; private set; }


        public UnitOfWork(EShoppingTutorialDbContext context)
        {
            _context = context;

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


        /// <summary>
        /// No matter an exception has been raised or not, this method always will dispose the DbContext 
        /// </summary>
        /// <returns></returns>
        public ValueTask DisposeAsync()
        {
            return _context.DisposeAsync();
        }
    }
}
