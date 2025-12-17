using NUnit.Framework;
using SharedKernel.Exceptions;
using EShoppingTutorial.Core.Domain.Entities;
using System;
using EShoppingTutorial.Core.Domain.ValueObjects;
using EShoppingTutorial.Core.Domain.Enums;

namespace EShoppingTutorial.UnitTests.Domain.Entities
{
    public class OrderShould
    {
        [Test]
        public void Test_InstantiatingOrder_WithEmptyOrderItems_ExpectsBusinessRuleBrokenException()
        {
            // arrange - act
            static void testDelegate() => new Order(new CustomerId(1), "Germany", []);

            // assert
            var ex = Assert.Throws<BusinessRuleBrokenException>(testDelegate);
        }

        [Test]
        public void Test_OrderItemsProperty_AddingOrderItemToReadOnlyCollection_ExpectsNotSupportedException()
        {
            // arrange
            ProductId productId = new(1);

            var order = new Order(new CustomerId(1), "Germany", [new OrderItem(productId, new Price(amount: 1, MoneyUnit.Dollar))]);

            // act
            void testDelegate() => order.OrderItems.Add(new OrderItem(productId, new Price(1, MoneyUnit.Dollar)));

            // assert
            var ex = Assert.Throws<NotSupportedException>(testDelegate);
        }

        [Test]
        public void Test_InstantiateOrder_WithOrderItems_ThatExccedsTotalPriceOf_10000_Dollar_ExpectsBusinessRuleBrokenException()
        {
            // arrange
            ProductId productId = new(1);

            var orderItem1 = new OrderItem(productId, new Price (amount: 5000, MoneyUnit.Dollar));

            var orderItem2 = new OrderItem(productId, new Price(amount: 6000, MoneyUnit.Dollar));

            // act
            void testDelegate()
            {
                new Order(new CustomerId(1), "Germany", [orderItem1, orderItem2]);
            }

            // assert
            var ex = Assert.Throws<BusinessRuleBrokenException>(testDelegate);

            Assert.That(ex.Message.Contains("maximum price", StringComparison.CurrentCultureIgnoreCase));
        }

        [Test]
        public void Test_InstantiateOrder_WithOrderItems_ThatExccedsTotalPriceOf_9000_Euro_ExpectsBusinessRuleBrokenException()
        {
            // arrange
            ProductId productId = new(1);

            var orderItem1 = new OrderItem(productId, new Price(5000, MoneyUnit.Dollar));

            var orderItem2 = new OrderItem(productId, new Price(6000, MoneyUnit.Dollar));

            // act
            void testDelegate()
            {
                new Order(new CustomerId(1), "Germany", new OrderItem[] { orderItem1, orderItem2 });
            }

            // assert
            var ex = Assert.Throws<BusinessRuleBrokenException>(testDelegate);

            Assert.That(ex.Message.ToLower().Contains("maximum price"));
        }
    }
}
