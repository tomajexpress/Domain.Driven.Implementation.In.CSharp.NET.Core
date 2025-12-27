namespace EShoppingTutorial.Core.Application.Orders.Commands.CreateOrder;

public class CreateOrderHandler(IUnitOfWork unitOfWork) : IRequestHandler<CreateOrderCommand>
{
    public async Task Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        unitOfWork.OrderRepository.Add(request.Order);

        await unitOfWork.CompleteAsync(cancellationToken).ConfigureAwait(false);
    }
}