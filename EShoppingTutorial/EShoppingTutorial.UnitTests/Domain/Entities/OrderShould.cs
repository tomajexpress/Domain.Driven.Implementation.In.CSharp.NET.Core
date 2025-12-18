using EShoppingTutorial.Core.Domain.Entities;
using EShoppingTutorial.Core.Domain.Enums;
using EShoppingTutorial.Core.Domain.ValueObjects;
using Moq;
using NUnit.Framework;
using SharedKernel.Exceptions;
using System;

namespace EShoppingTutorial.UnitTests.Domain.Entities
{
    public class OrderShould
    {
        [Test]
        public void InstantiatingOrder_WithEmptyOrderItems_ExpectsBusinessRuleBrokenException()
        {
            // arrange & act
            static void testDelegate() => new Order(new CustomerId(1), shippingAddress: "Germany", orderItems: []);

            // assert
            var ex = Assert.Throws<BusinessRuleBrokenException>(testDelegate);
            Assert.That(ex.Message.Contains("You must supply an Order Item", StringComparison.CurrentCultureIgnoreCase));
        }

        [Test]
        public void OrderItemsProperty_AddingOrderItemToReadOnlyCollection_ExpectsNotSupportedException()
        {
            // arrange
            var productIdMock = new ProductId(1);
            var priceMock = new Price(1, MoneyUnit.Dollar);
            var orderItemMock = new OrderItem(productIdMock, priceMock);

            var order = new Order(new CustomerId(1), "Germany", [orderItemMock]);

            // act
            void testDelegate() => order.OrderItems.Add(orderItemMock);

            // assert
            var ex = Assert.Throws<NotSupportedException>(testDelegate);
            Assert.That(ex.Message.Contains("Collection is read-only", StringComparison.CurrentCultureIgnoreCase));
        }

        [Test]
        public void InstantiateOrder_WithOrderItems_ThatExccedsTotalPriceOf_10000_Dollar_ExpectsBusinessRuleBrokenException()
        {
            // arrange
            ProductId productId = new(1);
            var price01 = new Price(5000, MoneyUnit.Dollar);
            var orderItem1 = Mock.Of<OrderItem>(x=> x.Price == price01);

            var price02 = new Price(6000, MoneyUnit.Dollar);
            var orderItem2 = Mock.Of<OrderItem>(x => x.Price == price02);

            // act
            void testDelegate()
            {
                new Order(new CustomerId(1), "Germany", [orderItem1, orderItem2]);
            }

            // assert
            var ex = Assert.Throws<BusinessRuleBrokenException>(testDelegate);

            Assert.That(ex.Message.Contains("Maximum price has been reached", StringComparison.CurrentCultureIgnoreCase));
        }

        [Test]
        public void AddOrderItem_ShouldAddItem_WhenValid()
        {
            // Arrange
            var order = Mock.Of<Order>(); // To directly test the AddOrderItem method, we mock the initialization step of the Order class.
            var productId = new ProductId(1);
            var orderItem = new OrderItem(productId, new Price(100, MoneyUnit.Euro));

            // Act
            order.AddOrderItem(orderItem);

            // Assert
            Assert.That(order.OrderItems, Contains.Item(orderItem));
        }

        [Test]
        public void AddOrderItem_ShouldThrowException_WhenExceedingMaxPrice()
        {
            // Arrange
            var order = Mock.Of<Order>(); // To directly test the AddOrderItem method, we mock the initialization step of the Order class.
            var productId = new ProductId(1);

            // Assume the max price for Euro is 9000 for this test
            var expensiveItem = new OrderItem(productId, new Price(15000, MoneyUnit.Euro));

            // Act & Assert
            var ex = Assert.Throws<BusinessRuleBrokenException>(() => order.AddOrderItem(expensiveItem));
            Assert.That(ex.Message.Contains("Maximum price has been reached", StringComparison.CurrentCultureIgnoreCase));
        }

        [Test]
        public void MarkAsShipped_ShouldSetStatusToShipped_WhenOrderIsCreated()
        {
            // Arrange
            var order = Mock.Of<Order>(x=> x.OrderStatus == OrderStatus.Created); // To directly test the AddOrderItem method, we mock the initialization step of the Order class.

            // Act
            order.MarkAsShipped();

            // Assert
            Assert.That(order.OrderStatus, Is.EqualTo(OrderStatus.Shipped));
        }

        [Test]
        public void MarkAsShipped_ShouldSetStatusToShipped_WhenOrderIsPending()
        {
            // Arrange
            var order = Mock.Of<Order>(x => x.OrderStatus == OrderStatus.Pending); // To directly test the AddOrderItem method, we mock the initialization step of the Order class.

            // Act
            order.MarkAsShipped();

            // Assert
            Assert.That(order.OrderStatus, Is.EqualTo(OrderStatus.Shipped));
        }

        [Test]
        public void MarkAsShipped_ShouldThrowException_WhenOrderIsNotCreatedOrPending()
        {
            // Arrange
            var order = Mock.Of<Order>(x => x.OrderStatus == OrderStatus.Delivered); // To directly test the AddOrderItem method, we mock the initialization step of the Order class.

            // Act & Assert
            var ex = Assert.Throws<BusinessRuleBrokenException>(order.MarkAsShipped);
            Assert.That(ex.Message.Contains("Order cannot be shipped", StringComparison.CurrentCultureIgnoreCase));
        }

    }
}
