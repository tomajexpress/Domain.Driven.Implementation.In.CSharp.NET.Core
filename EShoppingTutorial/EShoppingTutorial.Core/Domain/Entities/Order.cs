using System;
using System.Linq;
using System.Collections.Generic;
using SharedKernel.Exceptions;

namespace EShoppingTutorial.Core.Domain.Entities
{
    public class Order
    {
        public int Id { get; set; }

        public Guid? TrackingNumber { get; set; }

        public string ShippingAdress { get; set; }

        public DateTime OrderDate { get; set; }


        private List<OrderItem> _orderItems;
        public ICollection<OrderItem> OrderItems { get { return _orderItems.AsReadOnly(); } }


        protected Order()
        {
            _orderItems = new List<OrderItem>();
        }


        /// <summary>
        /// Throws Exception if Maximum price has been reached, or if no Order Item has been added to this Order
        /// </summary>
        /// <param name="orderItems"></param>
        public Order(IEnumerable<OrderItem> orderItems) : this()
        {
            if (!orderItems.Any())
                throw new BusinessRuleBrokenException("No Order Item has been added !");

            OrderDate = DateTime.Now;

            var maximumPriceLimit = MaximumPriceLimits.GetMaximumPriceLimit(orderItems.First().Price.Unit);

            foreach (var orderItem in orderItems)
                AddOrderItem(orderItem, maximumPriceLimit);
        }



        /// <summary>
        /// Throws Exception if Maximum price has been reached
        /// </summary>
        /// <param name="orderItem"></param>
        private void AddOrderItem(OrderItem orderItem, int maximumPriceLimit)
        {
            var sumPriceOfOrderItems = _orderItems.Sum(en => en.Price.Amount);

            if (sumPriceOfOrderItems + orderItem.Price.Amount > maximumPriceLimit)
            {
                throw new BusinessRuleBrokenException("Maximum price has been reached !");
            }

            _orderItems.Add(orderItem);
        }

    }
}
