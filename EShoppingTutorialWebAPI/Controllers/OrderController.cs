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
    public class OrderController(IMediator mediator) : ControllerBase
    {
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrder(int id)
        {
            var viewModel = await mediator.Send(new GetOrderByIdQuery(id));

            return viewModel is null ? NotFound() : Ok(viewModel);
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await mediator.Send(new GetAllOrdersQuery()));
        }

        [HttpPost]
        [Route("GetPaged")]
        public async Task<IActionResult> GetPaged([FromBody] QueryObjectParams queryObject)
        {
            return Ok(await mediator.Send(new GetPagedOrdersQuery(queryObject)));
        }

        [HttpPost("Add")]
        public async Task<IActionResult> Add([FromBody] CreateOrderCommand command)
        {
            // Validation happens automatically via Pipeline
            // Mapping to Entity happens inside the Handler
            return Ok(await mediator.Send(command));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var isDeleted = await mediator.Send(new DeleteOrderCommand(new OrderId(id)));

            return isDeleted ? Ok() : NotFound();
        }
    }
}