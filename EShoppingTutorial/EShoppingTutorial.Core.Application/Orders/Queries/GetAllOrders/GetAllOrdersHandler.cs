namespace EShoppingTutorial.Core.Application.Orders.Queries.GetAllOrders;

public class GetAllOrdersHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetAllOrdersQuery, IEnumerable<Order>>
{
    public async Task<IEnumerable<Order>> Handle(GetAllOrdersQuery request, CancellationToken cancellationToken)
    {
        return await unitOfWork.OrderRepository
            .GetAllAsync(x => x.OrderItems)
            .ConfigureAwait(false);
    }
}