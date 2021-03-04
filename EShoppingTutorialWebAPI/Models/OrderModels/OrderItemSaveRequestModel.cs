using System.ComponentModel.DataAnnotations;
using EShoppingTutorial.Core.Domain.ValueObjects;

namespace EShoppingTutorialWebAPI.Models.OrderModels
{
    public class OrderItemSaveRequestModel
    {
        [Required(ErrorMessage = "Please enter the product Id")]
        public int? ProductId { get; set; }

        [Required(ErrorMessage = "Please enter order item price")]
        public Price Price { get; set; }
    }
}
