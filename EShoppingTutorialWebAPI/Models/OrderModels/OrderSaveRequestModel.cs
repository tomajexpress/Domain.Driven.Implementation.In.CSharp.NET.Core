using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EShoppingTutorialWebAPI.Models.OrderModels
{
    public class OrderSaveRequestModel
    {
        [StringLength(100, ErrorMessage = " Maximum length for ShippingAdress is 100 characters ")]
        [Required(ErrorMessage = "Please enter the ShippingAdress")]
        public string ShippingAdress { get; set; }

        public IEnumerable<OrderItemSaveRequestModel> OrderItemsDtoModel { get; set; }
    }
}
