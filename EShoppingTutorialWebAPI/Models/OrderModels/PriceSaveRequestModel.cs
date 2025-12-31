namespace EShoppingTutorialWebAPI.Models.OrderModels;

public class PriceSaveRequestModel
{
    public decimal Amount { get; set; }

    public MoneyUnit Unit { get; set; } = MoneyUnit.UnSpecified;
}
