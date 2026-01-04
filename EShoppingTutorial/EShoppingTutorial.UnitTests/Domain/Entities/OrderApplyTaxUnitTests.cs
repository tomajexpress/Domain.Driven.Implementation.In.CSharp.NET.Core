namespace EShoppingTutorial.UnitTests.Domain;

[TestFixture]
public class OrderApplyTaxUnitTests
{
    private Mock<ITaxCalculationService> _taxServiceMock;

    [SetUp]
    public void Setup()
    {
        _taxServiceMock = new Mock<ITaxCalculationService>();
    }

    [Test]
    public async Task ApplyTaxAsync_ShouldAddTaxItem_WhenOrderIsCreated()
    {
        // Arrange
        var customerId = new CustomerId(1);
        var address = "123 Tech Street, New York";
        var items = new List<OrderItem>
        {
            new OrderItem(new ProductId(1), new Price(100, Currency.USD))
        };

        var order = new Order(customerId, address, items);

        var expectedTax = new Price(10, Currency.USD);

        // Setup the mock to return 10 USD tax
        _taxServiceMock
            .Setup(s => s.CalculateTaxAsync(address, 100, Currency.USD))
            .ReturnsAsync(expectedTax);

        // Act
        await order.ApplyTaxAsync(_taxServiceMock.Object);

        // Assert
        Assert.That(order.OrderItems.Count, Is.EqualTo(2)); // Original item + Tax item

        var taxItem = order.OrderItems.FirstOrDefault(x => x.ProductId.Value == 99999);
        Assert.That(taxItem, Is.Not.Null);
        Assert.That(taxItem.Price.Amount, Is.EqualTo(10));

        // Verify the service was actually called once
        _taxServiceMock.Verify(s => s.CalculateTaxAsync(address, 100, Currency.USD), Times.Once);
    }

    [Test]
    public void ApplyTaxAsync_ShouldThrowException_WhenOrderIsNotInCreatedState()
    {
        // Arrange
        var customerId = new CustomerId(1);

        var items = new List<OrderItem>
        {
            new OrderItem(new ProductId(1), new Price (100, Currency.USD))
        };

        var order = new Order(customerId, "Address", items);

        // Manually move state to Pending in order to simulate non-created state
        order.MarkAsPending();

        // Act & Assert
        var ex = Assert.ThrowsAsync<BusinessRuleBrokenException>(async () => await order.ApplyTaxAsync(_taxServiceMock.Object));

        Assert.That(ex.Message, Is.EqualTo("Tax can only be applied to new orders."));
    }
}