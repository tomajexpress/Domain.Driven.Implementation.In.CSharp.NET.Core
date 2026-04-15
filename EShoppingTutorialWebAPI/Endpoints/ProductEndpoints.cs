using EShoppingTutorial.Core.Application.Products.Commands;
using EShoppingTutorial.Core.Application.Products.Queries;

public static class ProductEndpoints
{
    public static void MapProductEndpoints(this IEndpointRouteBuilder app)
    {
        // Eine Route-Gruppe für alles, was mit '/api/product' zu tun hat
        var group = app.MapGroup("/api/product")
                       .WithTags("Products");

        // Der POST-Endpunkt zum Erstellen eines Produkts
        group.MapPost("/Add", async (CreateProductCommand command, IMediator mediator) =>
        {
            var newProductId = await mediator.Send(command);
            return Results.Ok(newProductId);
        });

        group.MapGet("/{id}", async (int id, IMediator mediator) =>
        {
            var viewModel = await mediator.Send(new GetProductByIdQuery(id));
            return viewModel is null ? Results.NotFound() : Results.Ok(viewModel);
        });

        group.MapGet("/GetAll", async (IMediator mediator) =>
            Results.Ok(await mediator.Send(new GetAllProductsQuery())));

        group.MapPost("/GetPaged", async (QueryObjectParams queryObject, IMediator mediator) =>
            Results.Ok(await mediator.Send(new GetPagedProductsQuery(queryObject))));
    }
}