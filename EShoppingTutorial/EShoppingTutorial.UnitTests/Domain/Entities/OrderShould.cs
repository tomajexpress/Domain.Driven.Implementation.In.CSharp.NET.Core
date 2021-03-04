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
            // act
            TestDelegate testDelegate = () => new Order(new OrderItem[] { });


            // assert
            var ex = Assert.Throws<BusinessRuleBrokenException>(testDelegate);
        }


        [Test]
        public void Test_OrderItemsProperty_AddingOrderItemToReadOnlyCollection_ExpectsNotSupportedException()
        {
            // arrange
            var order = new Order(new OrderItem[] { new OrderItem { } });


            // act
            TestDelegate testDelegate = () => order.OrderItems.Add(new OrderItem());


            // assert
            var ex = Assert.Throws<NotSupportedException>(testDelegate);
        }


        [Test]
        public void Test_InstantiateOrder_WithOrderItems_ThatExccedsTotalPriceOf_10000_Dollar_ExpectsBusinessRuleBrokenException()
        {
            // arrange

            var orderItem1 = new OrderItem { Price = new Price { Amount = 5000, Unit = MoneyUnit.Dollar } };

            var orderItem2 = new OrderItem { Price = new Price { Amount = 6000, Unit = MoneyUnit.Dollar } };

            // act
            TestDelegate testDelegate = () =>
            {
                new Order(new OrderItem[] { orderItem1, orderItem2 });
            };


            // assert
            var ex = Assert.Throws<BusinessRuleBrokenException>(testDelegate);

            Assert.That(ex.Message.ToLower().Contains("maximum price"));
        }


        [Test]
        public void Test_InstantiateOrder_WithOrderItems_ThatExccedsTotalPriceOf_9000_Euro_ExpectsBusinessRuleBrokenException()
        {
            // arrange

            var orderItem1 = new OrderItem { Price = new Price { Amount = 5000, Unit = MoneyUnit.Euro } };

            var orderItem2 = new OrderItem { Price = new Price { Amount = 4500, Unit = MoneyUnit.Euro } };

            // act
            TestDelegate testDelegate = () =>
            {
                new Order(new OrderItem[] { orderItem1, orderItem2 });
            };


            // assert
            var ex = Assert.Throws<BusinessRuleBrokenException>(testDelegate);

            Assert.That(ex.Message.ToLower().Contains("maximum price"));
        }


        [Test]
        public void Test_InstantiateOrder_WithOrderItems_That_MoneyUnitIsNotDefined_ExpectsBusinessRuleBrokenException()
        {
            // arrange
            var orderItem = new OrderItem { Price = new Price { Amount = 5000} };

            // act
            TestDelegate testDelegate = () =>
            {
                new Order(new OrderItem[] { orderItem });
            };

            // assert
            var ex = Assert.Throws<BusinessRuleBrokenException>(testDelegate);

            Assert.That(ex.Message.ToLower().Contains("money unit"));
        }

    }
}
