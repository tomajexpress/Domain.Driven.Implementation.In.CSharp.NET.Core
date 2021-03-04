using EShoppingTutorial.Core.Domain.ValueObjects;

namespace EShoppingTutorial.Core.Domain.Entities
{
    public class OrderItem
    {
        public OrderItem()
        {
            Price = new Price();
        }

        public int Id { get; set; }

        public int ProductId { get; set; }

        public Price Price { get; set; }

        public int OrderId { get; set; }
    }
}
