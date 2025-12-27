namespace EShoppingTutorial.Core.Application.Orders.Commands.DeleteOrder;

public class DeleteOrderHandler(IUnitOfWork unitOfWork) : IRequestHandler<DeleteOrderCommand, bool>
{
    public async Task<bool> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
    {
        var order = await unitOfWork.OrderRepository.GetByIdAsync(request.Id).ConfigureAwait(false);

        if (order is null) return false;

        unitOfWork.OrderRepository.Remove(order);

        await unitOfWork.CompleteAsync(cancellationToken).ConfigureAwait(false);

        return true;
    }
}