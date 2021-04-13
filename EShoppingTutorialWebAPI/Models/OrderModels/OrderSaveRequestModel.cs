using System.Collections.Generic;

namespace EShoppingTutorialWebAPI.Models.OrderModels
{
    public class OrderSaveRequestModel
    {
        /// <example>IRAN Tehran Persia</example>
        public string ShippingAdress { get; set; }

        public IEnumerable<OrderItemSaveRequestModel> OrderItemsDtoModel { get; set; }
    }
}
