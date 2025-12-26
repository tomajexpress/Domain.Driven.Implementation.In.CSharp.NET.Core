using GenericRepositoryEntityFramework;
using Microsoft.EntityFrameworkCore;

namespace EShoppingTutorial.Core.Persistence.Repositories;

public class OrderRepository(EShoppingTutorialDbContext context) : Repository<Order>(context), IOrderRepository
{
    public EShoppingTutorialDbContext EShoppingTutorialDbContext
    {
        get { return Context as EShoppingTutorialDbContext; }
    }

    public async Task<Order?> GetOrderWithItemsAsync(OrderId id)
    {
        return await context.Orders
            .Include(o => o.OrderItems)
            .FirstOrDefaultAsync(o => o.Id == id);
    }

    public async Task<IEnumerable<Order>> GetOrdersByCustomerIdAsync(CustomerId customerId)
    {
        return await context.Orders
            .Where(o => o.CustomerId == customerId)
            .OrderByDescending(o => o.OrderDate)
            .ToListAsync();
    }
}
