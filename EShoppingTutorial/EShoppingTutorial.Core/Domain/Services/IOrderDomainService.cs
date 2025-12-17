
using EShoppingTutorial.Core.Domain.Entities;
using SharedKernel.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EShoppingTutorial.Core.Domain.Services
{
    public interface IOrderDomainService
    {
        Task<Order?> GetOrderByIdAsync(int id);
        Task<IEnumerable<Order>> GetAllOrdersAsync();
        Task<QueryResult<Order>> GetPagedOrdersAsync(QueryObjectParams queryObject);
        Task AddOrderAsync(Order order);
        Task<bool> DeleteOrderAsync(int id);
    }
}