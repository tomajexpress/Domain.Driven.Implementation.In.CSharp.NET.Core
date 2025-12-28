using EShoppingTutorial.Core.Application.Orders.Commands.CreateOrder;
using EShoppingTutorial.Core.Application.Orders.Commands.DeleteOrder;
using EShoppingTutorial.Core.Application.Orders.Queries.GetAllOrders;
using EShoppingTutorial.Core.Application.Orders.Queries.GetOrderById;
using EShoppingTutorial.Core.Application.Orders.Queries.GetPagedOrders;

namespace EShoppingTutorialWebAPI.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class OrderController(IMapper mapper, IMediator mediator) : ControllerBase
    {
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrder(int id)
        {
            var order = await mediator.Send(new GetOrderByIdQuery(new OrderId(id)));

            if (order == null) return NotFound();

            return Ok(mapper.Map<OrderViewModel>(order));
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var orders = await mediator.Send(new GetAllOrdersQuery());

            var mappedOrders = mapper.Map<IEnumerable<OrderViewModel>>(orders);

            return Ok(new QueryResult<OrderViewModel>(mappedOrders, mappedOrders.Count()));
        }

        [HttpPost]
        [Route("GetPaged")]
        public async Task<IActionResult> GetPaged([FromBody] QueryObjectParams queryObject)
        {
            var queryResult = await mediator.Send(new GetPagedOrdersQuery(queryObject));

            var mappedEntities = mapper.Map<IEnumerable<OrderViewModel>>(queryResult.Entities);

            return Ok(new QueryResult<OrderViewModel>(mappedEntities, queryResult.TotalCount));
        }

        [HttpPost("Add")]
        public async Task<IActionResult> Add([FromBody] OrderSaveRequestModel orderResource)
        {
            var order = mapper.Map<Order>(orderResource);

            await mediator.Send(new CreateOrderCommand(order));

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var isDeleted = await mediator.Send(new DeleteOrderCommand(new OrderId(id)));

            return isDeleted ? Ok() : NotFound();
        }
    }
}
