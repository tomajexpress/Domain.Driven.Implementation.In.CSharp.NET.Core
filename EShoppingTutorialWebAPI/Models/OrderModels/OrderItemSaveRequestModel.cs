namespace EShoppingTutorialWebAPI.Models.OrderModels;

public class OrderItemSaveRequestModel
{
    public int ProductId { get; set; }

    public required PriceSaveRequestModel Price { get; set; }
}
