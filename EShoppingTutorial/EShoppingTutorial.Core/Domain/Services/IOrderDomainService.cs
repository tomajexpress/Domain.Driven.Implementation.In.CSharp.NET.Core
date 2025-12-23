namespace EShoppingTutorial.Core.Domain.Services;

public interface IOrderDomainService
{
    Task<Order?> GetOrderByIdAsync(OrderId id);
    Task<IEnumerable<Order>> GetAllOrdersAsync();
    Task<QueryResult<Order>> GetPagedOrdersAsync(QueryObjectParams queryObject);
    Task AddOrderAsync(Order order);
    Task<bool> DeleteOrderAsync(OrderId id);
}