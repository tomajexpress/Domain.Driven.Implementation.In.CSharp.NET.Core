using EShoppingTutorial.Core.Domain.Entities;
using EShoppingTutorial.Core.Domain.ValueObjects;
using SharedKernel.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EShoppingTutorial.Core.Domain.Services
{
    public class OrderDomainService(IUnitOfWork unitOfWork) : IOrderDomainService
    {
        public async Task<Order?> GetOrderByIdAsync(OrderId id)
        {
            return await unitOfWork.OrderRepository
                .GetAsync(predicate: x => x.Id == id, includes: x => x.OrderItems)
                .ConfigureAwait(false);
        }

        public async Task<IEnumerable<Order>> GetAllOrdersAsync()
        {
            return await unitOfWork.OrderRepository
                .GetAllAsync(x => x.OrderItems)
                .ConfigureAwait(false);
        }

        public async Task<QueryResult<Order>> GetPagedOrdersAsync(QueryObjectParams queryObject)
        {
            return await unitOfWork.OrderRepository
                .GetPageAsync(queryObject, predicate: null, includes: x => x.OrderItems)
                .ConfigureAwait(false);
        }

        public async Task AddOrderAsync(Order order)
        {
            unitOfWork.OrderRepository.Add(order);
            await unitOfWork.CompleteAsync().ConfigureAwait(false);
        }

        public async Task<bool> DeleteOrderAsync(OrderId id)
        {
            var order = await unitOfWork.OrderRepository
                .GetByIdAsync(id)
                .ConfigureAwait(false);

            if (order is null)
            {
                return false;
            }

            unitOfWork.OrderRepository.Remove(order);
            await unitOfWork.CompleteAsync().ConfigureAwait(false);
            return true;
        }
    }
}