namespace EShoppingTutorial.Core.Application.Orders.Queries.GetOrderById;

public class GetOrderByIdHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetOrderByIdQuery, Order?>
{
    public async Task<Order?> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
    {
        return await unitOfWork.OrderRepository
            .GetAsync(predicate: x => x.Id == request.Id, includes: x => x.OrderItems)
            .ConfigureAwait(false);
    }
}