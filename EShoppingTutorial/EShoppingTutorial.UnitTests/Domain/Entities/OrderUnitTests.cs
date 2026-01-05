namespace EShoppingTutorial.UnitTests.Domain.Entities;

[TestFixture]
public class OrderUnitTests
{
    [Test]
    public void InstantiatingOrder_WithEmptyOrderItems_ExpectsBusinessRuleBrokenException()
    {
        // arrange & act
        static void testDelegate() => new Order(new CustomerId(1), Mock.Of<Address>(), orderItems: []);

        // assert
        var ex = Assert.Throws<BusinessRuleBrokenException>(testDelegate);
        Assert.That(ex.Message.Contains("Order must have at least one item", StringComparison.CurrentCultureIgnoreCase));
    }

    [Test]
    public void OrderItemsProperty_AddingOrderItemToReadOnlyCollection_ExpectsNotSupportedException()
    {
        // arrange
        var productIdMock = new ProductId(1);
        var priceMock = new Price(1, Currency.USD);
        var orderItemMock = new OrderItem(productIdMock, priceMock);

        var order = new Order(new CustomerId(1), Mock.Of<Address>(), [orderItemMock]);

        // act
        void testDelegate() => order.OrderItems.Add(orderItemMock);

        // assert
        var ex = Assert.Throws<NotSupportedException>(testDelegate);
        Assert.That(ex.Message.Contains("Collection is read-only", StringComparison.CurrentCultureIgnoreCase));
    }

    [Test]
    public void AddOrderItem_ShouldThrowException_WhenPriceExceedsLimit()
    {
        // 1. Arrange: Setup our entities and a price that exceeds the USD limit ($10,000)
        var customerId = new CustomerId(1);

        // To directly test the AddOrderItem method, we mock the initialization step of the Order class.
        var order = Mock.Of<Order>(); 

        var expensivePrice = new Price(11000m, Currency.USD);
        var productId = new ProductId(99);
        var orderItem = new OrderItem(productId, expensivePrice);

        // 2. Act & 3. Assert: Verify the exception is thrown with the correct message
        var ex = Assert.Throws<BusinessRuleBrokenException>(() => order.AddOrderItem(orderItem));

        Assert.That(ex.Message, Does.Contain("Maximum price has been reached"));
    }

    [TestCase(OrderStatus.Pending)]
    public void MarkAsShipped_ShouldSetStatusToShipped_WhenOrderIsPending(OrderStatus initialStatus)
    {
        // Arrange
        var order = Mock.Of<Order>(x => x.OrderStatus == initialStatus); // To directly test the MarkAsShipped method, we mock the initialization step of the Order class.

        // Act
        order.MarkAsShipped();

        // Assert
        Assert.That(order.OrderStatus, Is.EqualTo(OrderStatus.Shipped));
    }

    [TestCase(OrderStatus.Shipped)]
    [TestCase(OrderStatus.Delivered)]
    [TestCase(OrderStatus.Cancelled)]
    public void MarkAsShipped_ShouldThrowException_WhenOrderIsNotCreatedOrPendingState(OrderStatus initialStatus)
    {
        // Arrange
        var order = Mock.Of<Order>(x => x.OrderStatus == initialStatus); // To directly test the MarkAsShipped method, we mock the initialization step of the Order class.

        // Act & Assert
        var ex = Assert.Throws<BusinessRuleBrokenException>(order.MarkAsShipped);
        Assert.That(ex.Message.Contains("Order cannot be shipped", StringComparison.CurrentCultureIgnoreCase));
    }

    [Test]
    public void AddOrderItem_ShouldAddItem_WhenValid()
    {
        // Arrange
        var order = Mock.Of<Order>(); // To directly test the AddOrderItem method, we mock the initialization step of the Order class.
        var productId = new ProductId(1);
        var orderItem = new OrderItem(productId, new Price(100, Currency.EUR));

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
        var expensiveItem = new OrderItem(productId, new Price(15000, Currency.EUR));

        // Act & Assert
        var ex = Assert.Throws<BusinessRuleBrokenException>(() => order.AddOrderItem(expensiveItem));
        Assert.That(ex.Message.Contains("Maximum price has been reached", StringComparison.CurrentCultureIgnoreCase));
    }

    [Test]
    public void MarkAsCancelled_ShouldSetStatusToCancelled_WhenOrderIsCreatedOrPending()
    {
        // Arrange
        var order = Mock.Of<Order>(x => x.OrderStatus == OrderStatus.Created); // To directly test the MarkAsCancelled method, we mock the initialization step of the Order class.

        // Act
        order.MarkAsCancelled();

        // Assert
        Assert.That(order.OrderStatus, Is.EqualTo(OrderStatus.Cancelled));
    }

    [TestCase(OrderStatus.Shipped)]
    [TestCase(OrderStatus.Delivered)]
    [TestCase(OrderStatus.Cancelled)]
    [TestCase(OrderStatus.Pending)]
    public void MarkAsCancelled_ShouldThrowException_WhenOrderIsNotCreatedState(OrderStatus initialStatus)
    {
        // Arrange
        var order = Mock.Of<Order>(x => x.OrderStatus == initialStatus); // To directly test the MarkAsCancelled method, we mock the initialization step of the Order class.

        // Act & Assert
        var ex = Assert.Throws<BusinessRuleBrokenException>(order.MarkAsCancelled);
        Assert.That(ex.Message.Contains("Only orders in 'Created' state can be cancelled", StringComparison.CurrentCultureIgnoreCase));
    }

    [TestCase(OrderStatus.Shipped)]
    [TestCase(OrderStatus.Delivered)]
    [TestCase(OrderStatus.Cancelled)]
    [TestCase(OrderStatus.Pending)]
    public void MarkAsSuspended_ShouldThrowException_WhenOrderIsNotCreatedState(OrderStatus initialStatus)
    {
        // Arrange
        var order = Mock.Of<Order>(x => x.OrderStatus == initialStatus); // To directly test the MarkAsPending method, we mock the initialization step of the Order class.

        // Act & Assert
        var ex = Assert.Throws<BusinessRuleBrokenException>(order.MarkAsPending);
        Assert.That(ex.Message.Contains("Only orders in 'Created' state can be suspended", StringComparison.CurrentCultureIgnoreCase));
    }
}