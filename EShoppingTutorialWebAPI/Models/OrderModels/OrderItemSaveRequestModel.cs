using EShoppingTutorial.Core.Domain.ValueObjects;

namespace EShoppingTutorialWebAPI.Models.OrderModels
{
    public class OrderItemSaveRequestModel
    {
        /// <example>1</example>
        public int? ProductId { get; set; }
   
        public PriceSaveRequestModel Price { get; set; }
    }
}
