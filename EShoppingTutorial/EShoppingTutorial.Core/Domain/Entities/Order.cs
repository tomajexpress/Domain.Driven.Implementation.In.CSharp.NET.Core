using EShoppingTutorial.Core.Domain.Enums;
using EShoppingTutorial.Core.Domain.InvariantRules;
using EShoppingTutorial.Core.Domain.ValueObjects;
using SharedKernel.Exceptions;
using SharedKernel.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EShoppingTutorial.Core.Domain.Entities
{
    public class Order : IAggregateRoot
    {
        private readonly List<OrderItem> _orderItems = new();

        public OrderId Id { get; protected set; }
        public Guid? TrackingNumber { get; protected set; }
        public string ShippingAddress { get; protected set; }
        public CustomerId CustomerId { get; protected set; }
        public DateTime OrderDate { get; protected set; } = DateTime.Now;
        public OrderStatus OrderStatus { get; protected set; }
        public ICollection<OrderItem> OrderItems => _orderItems.AsReadOnly();

        protected Order() { }

        public Order(CustomerId customerId, string shippingAddress, IEnumerable<OrderItem> orderItems) : this()
        {
            ValidateCustomerId(customerId);
            ValidateShippingAddress(shippingAddress);
            ValidateOrderItems(orderItems);

            CustomerId = customerId;
            ShippingAddress = shippingAddress;
            TrackingNumber = Guid.NewGuid();
            OrderDate = DateTime.Now;
            OrderStatus = OrderStatus.Created;

            AddOrderItems(orderItems);
        }

        /// <summary>
        /// Creates a new order for the specified customer with the provided shipping address.
        /// </summary>
        public static Order Create(CustomerId customerId, string shippingAddress)
        {
            ValidateCustomerId(customerId);
            ValidateShippingAddress(shippingAddress);

            return new Order
            {
                CustomerId = customerId,
                ShippingAddress = shippingAddress,
                TrackingNumber = Guid.NewGuid(),
                OrderDate = DateTime.Now,
                OrderStatus = OrderStatus.Created
            };
        }

        /// <summary>
        /// Adds the specified order item to the current order.
        /// </summary>
        /// <param name="orderItem">The order item to add. Cannot be null.</param>
        /// <exception cref="BusinessRuleBrokenException">Thrown if <paramref name="orderItem"/> is null or if the order item violates business rules, such as
        /// exceeding the maximum allowed price.</exception>
        public void AddOrderItem(OrderItem orderItem)
        {
            if (orderItem is null)
                throw new BusinessRuleBrokenException("You must supply an Order Item!");

            ValidateMaxPriceLimit(orderItem);
            _orderItems.Add(orderItem);
        }

        public void MarkAsCancelled()
        {
            if (OrderStatus != OrderStatus.Created)
                throw new BusinessRuleBrokenException("Only Created orders can be cancelled.");

            OrderStatus = OrderStatus.Cancelled;
        }

        public void MarkAsShipped()
        {
            if (OrderStatus != OrderStatus.Created && OrderStatus != OrderStatus.Pending)
                throw new BusinessRuleBrokenException($"Order cannot be shipped in its current state: {OrderStatus}.");

            OrderStatus = OrderStatus.Shipped;
        }

        private void AddOrderItems(IEnumerable<OrderItem> orderItems)
        {
            foreach (var orderItem in orderItems)
            {
                ValidateMaxPriceLimit(orderItem);
                _orderItems.Add(orderItem);
            }
        }

        private void ValidateMaxPriceLimit(OrderItem orderItem)
        {
            var unit = orderItem.Price.Unit;
            var maximumPriceLimit = MaximumPriceLimits.GetMaximumPriceLimit(unit);
            var sumPriceOfOrderItems = _orderItems.Sum(en => en.Price.Amount);

            if (sumPriceOfOrderItems + orderItem.Price.Amount > maximumPriceLimit)
                throw new BusinessRuleBrokenException("Maximum price has been reached!");
        }

        private static void ValidateCustomerId(CustomerId customerId)
        {
            if (customerId is null || customerId.Value == 0)
                throw new BusinessRuleBrokenException("You must supply Customer Id!");
        }

        private static void ValidateShippingAddress(string shippingAddress)
        {
            if (string.IsNullOrWhiteSpace(shippingAddress))
                throw new BusinessRuleBrokenException("You must supply Shipping Address!");
        }

        private static void ValidateOrderItems(IEnumerable<OrderItem> orderItems)
        {
            if (orderItems is null || !orderItems.Any())
                throw new BusinessRuleBrokenException("You must supply an Order Item!");
        }
    }
}
