namespace EShoppingTutorial.Core.Application.Orders.Queries.GetOrderById;

public class GetOrderByIdHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<GetOrderByIdQuery, OrderViewModel?>
{
    public async Task<OrderViewModel?> Handle(GetOrderByIdQuery request, CancellationToken ct)
    {
        var order = await unitOfWork.OrderRepository
            .GetAsync(predicate: x => x.Id == new OrderId(request.Id), includes: x => x.OrderItems)
            .ConfigureAwait(false);

        if (order == null) return null;

        return mapper.Map<OrderViewModel>(order);
    }
}