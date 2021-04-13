using EShoppingTutorial.Core.Domain.ValueObjects;

namespace EShoppingTutorialWebAPI.Models.OrderModels
{
    public class OrderItemSaveRequestModel
    {
        /// <example>IRAN Tehran Persia</example>
        public int? ProductId { get; set; }
   
        public Price Price { get; set; }
    }
}
