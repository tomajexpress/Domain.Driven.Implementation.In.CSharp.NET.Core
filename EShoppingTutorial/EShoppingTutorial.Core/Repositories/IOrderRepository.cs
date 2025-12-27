namespace EShoppingTutorial.Core.Domain.Repositories;

public interface IOrderRepository : IGenericRepository<Order>
{
    Task<Order?> GetOrderWithItemsAsync(OrderId id);

    Task<IEnumerable<Order>> GetOrdersByCustomerIdAsync(CustomerId customerId);
}