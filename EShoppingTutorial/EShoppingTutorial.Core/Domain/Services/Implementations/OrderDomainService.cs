using EShoppingTutorial.Core.Domain.Entities;
using SharedKernel.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EShoppingTutorial.Core.Domain.Services
{
    public class OrderDomainService : IOrderDomainService
    {
        private readonly IUnitOfWork _unitOfWork;

        public OrderDomainService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Order?> GetOrderByIdAsync(int id)
        {
            return await _unitOfWork.OrderRepository.GetByIdAsync(id).ConfigureAwait(false);
        }

        public async Task<IEnumerable<Order>> GetAllOrdersAsync()
        {
            return await _unitOfWork.OrderRepository.GetAllAsync(en => en.OrderItems).ConfigureAwait(false);
        }

        public async Task<QueryResult<Order>> GetPagedOrdersAsync(QueryObjectParams queryObject)
        {
            return await _unitOfWork.OrderRepository.GetPageAsync(queryObject).ConfigureAwait(false);
        }

        public async Task AddOrderAsync(Order order)
        {
            _unitOfWork.OrderRepository.Add(order);
            await _unitOfWork.CompleteAsync().ConfigureAwait(false);
        }

        public async Task<bool> DeleteOrderAsync(int id)
        {
            var order = await _unitOfWork.OrderRepository.GetByIdAsync(id).ConfigureAwait(false);

            if (order is null)
            {
                return false;
            }

            _unitOfWork.OrderRepository.Remove(order);
            await _unitOfWork.CompleteAsync().ConfigureAwait(false);
            return true;
        }
    }
}