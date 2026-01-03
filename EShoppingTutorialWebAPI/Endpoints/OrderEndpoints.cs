using EShoppingTutorial.Core.Application.Orders.Commands.CreateOrder;
using EShoppingTutorial.Core.Application.Orders.Commands.DeleteOrder;
using EShoppingTutorial.Core.Application.Orders.Queries.GetAllOrders;
using EShoppingTutorial.Core.Application.Orders.Queries.GetOrderById;
using EShoppingTutorial.Core.Application.Orders.Queries.GetPagedOrders;

public static class OrderEndpoints
{
    public static void MapOrderEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/order")
                       .WithTags("Orders");

        group.MapGet("/{id}", async (int id, IMediator mediator) =>
        {
            var viewModel = await mediator.Send(new GetOrderByIdQuery(id));
            return viewModel is null ? Results.NotFound() : Results.Ok(viewModel);
        });

        group.MapGet("/GetAll", async (IMediator mediator) =>
            Results.Ok(await mediator.Send(new GetAllOrdersQuery())));

        group.MapPost("/GetPaged", async (QueryObjectParams queryObject, IMediator mediator) =>
            Results.Ok(await mediator.Send(new GetPagedOrdersQuery(queryObject))));

        group.MapPost("/Add", async (CreateOrderCommand command, IMediator mediator) =>
            Results.Ok(await mediator.Send(command)));

        group.MapDelete("/{id}", async (int id, IMediator mediator) =>
        {
            var isDeleted = await mediator.Send(new DeleteOrderCommand(new OrderId(id)));
            return isDeleted ? Results.Ok() : Results.NotFound();
        });
    }
}