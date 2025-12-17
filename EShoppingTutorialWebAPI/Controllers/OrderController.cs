using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using EShoppingTutorial.Core.Domain.Entities;
using EShoppingTutorialWebAPI.Models.OrderModels;
using SharedKernel.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using EShoppingTutorial.Core.Domain.Services;

namespace EShoppingTutorialWebAPI.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IOrderDomainService _orderDomainService;

        public OrderController(IMapper mapper, IOrderDomainService orderDomainService)
        {
            _mapper = mapper;
            _orderDomainService = orderDomainService;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetOrder(int id)
        {
            var order = await _orderDomainService.GetOrderByIdAsync(id).ConfigureAwait(false);

            if (order == null)
                return NotFound();

            var mappedOrder = _mapper.Map<OrderViewModel>(order);

            return Ok(mappedOrder);
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var orders = await _orderDomainService.GetAllOrdersAsync().ConfigureAwait(false);

            if (orders is null)
                return NotFound();

            var mappedOrders = _mapper.Map<IEnumerable<OrderViewModel>>(orders);

            return Ok(new QueryResult<OrderViewModel>(mappedOrders, mappedOrders.Count()));
        }

        [HttpPost]
        [Route("GetPaged")]
        public async Task<IActionResult> GetPaged([FromBody] QueryObjectParams queryObject)
        {
            var queryResult = await _orderDomainService.GetPagedOrdersAsync(queryObject).ConfigureAwait(false);

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

            await _orderDomainService.AddOrderAsync(order).ConfigureAwait(false);

            return Ok();
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var isDeleted = await _orderDomainService.DeleteOrderAsync(id).ConfigureAwait(false);

            if (!isDeleted)
            {
                return NotFound();
            }

            return Ok();
        }
    }
}
