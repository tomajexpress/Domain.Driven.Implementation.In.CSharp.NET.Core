using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using EShoppingTutorial.Core.Domain;
using EShoppingTutorial.Core.Domain.Entities;
using EShoppingTutorialWebAPI.Models.OrderModels;
using SharedKernel.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace EShoppingTutorialWebAPI.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public OrderController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetOrder(int id)
        {
            var order = await _unitOfWork.OrderRepository.GetByIdAsync(id).ConfigureAwait(false);

            if (order == null)
                return NotFound();

            var mappedOrder = _mapper.Map<OrderViewModel>(order);

            return Ok(mappedOrder);
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var orders = await _unitOfWork.OrderRepository.GetAllAsync(en => en.OrderItems).ConfigureAwait(false);

            if (orders is null)
                return NotFound();

            var mappedOrders = _mapper.Map<IEnumerable<OrderViewModel>>(orders);

            return Ok(new QueryResult<OrderViewModel>(mappedOrders, mappedOrders.Count()));
        }

        [HttpPost]
        [Route("GetPaged")]
        public async Task<IActionResult> GetPaged([FromBody] QueryObjectParams queryObject)
        {
            var queryResult = await _unitOfWork.OrderRepository.GetPageAsync(queryObject).ConfigureAwait(false);

            if (queryResult is null)
                return NotFound();

            var mappedOrders = _mapper.Map<IEnumerable<OrderViewModel>>(queryResult.Entities);

            return Ok(new QueryResult<OrderViewModel>(mappedOrders, queryResult.TotalCount));
        }

        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> Add([FromBody] OrderSaveRequestModel orderResource)
        {
            var order = _mapper.Map<OrderSaveRequestModel, Order>(orderResource);

            _unitOfWork.OrderRepository.Add(order);

            await _unitOfWork.CompleteAsync().ConfigureAwait(false);

            return Ok();
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var order = await _unitOfWork.OrderRepository.GetByIdAsync(id).ConfigureAwait(false);

            if (order is null)
                return NotFound();

            _unitOfWork.OrderRepository.Remove(order);

            await _unitOfWork.CompleteAsync().ConfigureAwait(false);

            return Ok();
        }
    }
}
