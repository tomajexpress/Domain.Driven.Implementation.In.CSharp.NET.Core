namespace EShoppingTutorial.Core.Application.Orders.Queries;

public record OrderViewModel(
    int Id,
    Guid TrackingNumber,
    string ShippingAddress,
    DateTime OrderDate,
    IEnumerable<OrderItemViewModel> OrderItems);