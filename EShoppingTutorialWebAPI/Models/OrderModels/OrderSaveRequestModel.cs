namespace EShoppingTutorialWebAPI.Models.OrderModels;

public class OrderSaveRequestModel
{
    public string ShippingAdress { get; set; } = string.Empty;

    public int CustomerId { get; set; }

    public IEnumerable<OrderItemSaveRequestModel> OrderItemsDtoModel { get; set; }
}
