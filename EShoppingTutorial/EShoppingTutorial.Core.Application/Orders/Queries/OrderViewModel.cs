namespace EShoppingTutorial.Core.Application.Orders.Queries;

public record OrderViewModel(
    int Id,
    Guid TrackingNumber,
    AddressDto ShippingAddress,
    DateTime OrderDate,
    IEnumerable<OrderItemViewModel> OrderItems);