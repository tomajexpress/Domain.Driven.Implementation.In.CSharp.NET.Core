using System;
using System.Linq;
using System.Collections.Generic;
using SharedKernel.Exceptions;
using SharedKernel.Models;

namespace EShoppingTutorial.Core.Domain.Entities
{
    public class Order : IAggregateRoot
    {
        public int Id { get; protected set; }

        public Guid? TrackingNumber { get; protected set; }

        public string ShippingAdress { get; protected set; }

        public DateTime OrderDate { get; protected set; }


        private List<OrderItem> _orderItems;
        public ICollection<OrderItem> OrderItems { get { return _orderItems.AsReadOnly(); } }


        protected Order() // For Entity Framework Core
        {
            _orderItems = new List<OrderItem>();
        }


        /// <summary>
        /// Throws Exception if Maximum price has been reached, or if no Order Item has been added to this Order
        /// </summary>
        /// <param name="orderItems"></param>
        public Order(string shippingAdress, IEnumerable<OrderItem> orderItems) : this()
        {
            CheckForBrokenRules(shippingAdress, orderItems);

            AddOrderItems(orderItems);


            ShippingAdress = shippingAdress;

            TrackingNumber = Guid.NewGuid();

            OrderDate = DateTime.Now;
        }

        private void CheckForBrokenRules(string shippingAdress, IEnumerable<OrderItem> orderItems)
        {
            if (string.IsNullOrWhiteSpace(shippingAdress))
                throw new BusinessRuleBrokenException("You must supply ShippingAdress!");

            if (orderItems is null || (!orderItems.Any()))
                throw new BusinessRuleBrokenException("You must supply an Order Item!");
        }

        private void AddOrderItems(IEnumerable<OrderItem> orderItems)
        {
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
