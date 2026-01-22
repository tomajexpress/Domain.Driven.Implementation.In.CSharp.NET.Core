namespace EShoppingTutorial.Core.Application.Orders.Commands.CreateOrder;

internal class CreateOrderHandler(IUnitOfWork unitOfWork, 
            ITaxCalculationService taxCalculationService,
            IMapper mapper) 
            : IRequestHandler<CreateOrderCommand, int>
{
    public async Task<int> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        // The handler executes the logic of the domain service and coordinates the asynchronous tax calculation.

        var items = mapper.Map<List<OrderItem>>(request.Items);

        var order = new Order(
            new CustomerId(request.CustomerId),
            new Address(request.Street, request.City, request.Country, request.ZipCode),
            items
        );

        // This is where the 'External State' dependency is handled
        await order.ApplyTaxAsync(taxCalculationService);

        unitOfWork.OrderRepository.Add(order);

        await unitOfWork.CompleteAsync(cancellationToken).ConfigureAwait(false);

        return order.Id.Value;
    }
}