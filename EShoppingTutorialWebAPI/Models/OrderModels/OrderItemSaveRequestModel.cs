using EShoppingTutorial.Core.Domain.ValueObjects;

namespace EShoppingTutorialWebAPI.Models.OrderModels
{
    public class OrderItemSaveRequestModel
    {
        public int? ProductId { get; set; }
   
        public PriceSaveRequestModel Price { get; set; }
    }
}
