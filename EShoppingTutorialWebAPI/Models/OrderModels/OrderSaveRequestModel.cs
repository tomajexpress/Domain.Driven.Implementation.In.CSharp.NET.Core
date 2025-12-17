using System.Collections.Generic;

namespace EShoppingTutorialWebAPI.Models.OrderModels
{
    public class OrderSaveRequestModel
    {
        public string ShippingAdress { get; set; }

        public int CustomerId { get; set; }

        public IEnumerable<OrderItemSaveRequestModel> OrderItemsDtoModel { get; set; }
    }
}
