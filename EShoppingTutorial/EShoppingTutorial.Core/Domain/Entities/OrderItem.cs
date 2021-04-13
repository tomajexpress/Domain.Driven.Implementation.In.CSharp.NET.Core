using EShoppingTutorial.Core.Domain.ValueObjects;

namespace EShoppingTutorial.Core.Domain.Entities
{
    public class OrderItem
    {
        public int Id { get; protected set; }

        public int ProductId { get; protected set; }

        public Price Price { get; protected set; }

        public int OrderId { get; protected set; }


        protected OrderItem()
        {
            
        }

        public OrderItem(int productId, Price price)
        {
            ProductId = productId;

            Price = price;
        }
    }
}
