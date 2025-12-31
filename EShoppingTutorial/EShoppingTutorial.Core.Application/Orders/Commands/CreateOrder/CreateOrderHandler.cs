namespace EShoppingTutorial.Core.Application.Orders.Commands.CreateOrder;

public class CreateOrderHandler(IUnitOfWork unitOfWork, 
            ITaxCalculationService taxCalculationService) 
            : IRequestHandler<CreateOrderCommand>
{
    public async Task Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        // The handler executes the logic of the domain service and coordinates the asynchronous tax calculation.

        await request.Order.ApplyTaxAsync(taxCalculationService);

        unitOfWork.OrderRepository.Add(request.Order);

        await unitOfWork.CompleteAsync(cancellationToken).ConfigureAwait(false);
    }
}