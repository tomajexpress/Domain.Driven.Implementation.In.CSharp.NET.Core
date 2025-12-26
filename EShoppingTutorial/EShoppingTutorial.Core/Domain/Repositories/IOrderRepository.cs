using GenericRepositoryEntityFramework;

namespace EShoppingTutorial.Core.Domain.Repositories;

public interface IOrderRepository : IRepository<Order>
{
    Task<Order?> GetOrderWithItemsAsync(OrderId id);

    Task<IEnumerable<Order>> GetOrdersByCustomerIdAsync(CustomerId customerId);
}