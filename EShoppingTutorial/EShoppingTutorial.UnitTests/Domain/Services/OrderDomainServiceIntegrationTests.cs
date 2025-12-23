using EShoppingTutorial.Core.Domain.Entities;
using EShoppingTutorial.Core.Domain.Enums;
using EShoppingTutorial.Core.Domain.Services;
using EShoppingTutorial.Core.Domain.ValueObjects;
using EShoppingTutorial.Core.Persistence;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace EShoppingTutorial.UnitTests.Domain.Services;

[TestFixture]
public class OrderDomainServiceIntegrationTests
{
    private DbContextOptionsBuilder<EShoppingTutorialDbContext> _builder;

    [SetUp]
    public void Setup()
    {
        // Creates a new unique in-memory database for each test
        _builder = new DbContextOptionsBuilder<EShoppingTutorialDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString());
    }

    private EShoppingTutorialDbContext CreateContext() => new(_builder.Options);

    [Test]
    public async Task AddOrderAsync_ShouldPersistOrderToDatabase()
    {
        // Arrange
        using var context = CreateContext();
        var unitOfWork = new UnitOfWork(context);
        var service = new OrderDomainService(unitOfWork);
        var customerId = new CustomerId(1);
        var orderItems = new[] { new OrderItem(new ProductId(1), new Price(50, MoneyUnit.EUR)) };
        var order = new Order(customerId, shippingAddress: "Germany", orderItems);

        // Act
        await service.AddOrderAsync(order);

        // Assert: Verify using a fresh context to ensure it's in the DB
        using var assertContext = CreateContext();
        var savedOrder = await assertContext.Orders.FindAsync(order.Id);

        Assert.That(savedOrder, Is.Not.Null);
        Assert.That(savedOrder.CustomerId, Is.EqualTo(customerId));
    }

    [Test]
    public async Task DeleteOrderAsync_WhenOrderExists_ShouldRemoveFromDatabase()
    {
        // Arrange: Seed the database
        var customerId = new CustomerId(1);
        var orderItems = new[] { new OrderItem(new ProductId(1), new Price(50, MoneyUnit.EUR)) };
        var order = new Order(customerId, shippingAddress: "Germany", orderItems);

        using (var context = CreateContext())
        {
            context.Orders.Add(order);
            await context.SaveChangesAsync();
        }

        using var testContext = CreateContext();
        var service = new OrderDomainService(new UnitOfWork(testContext));

        // Act
        var result = await service.DeleteOrderAsync(order.Id);

        // Assert
        Assert.That(result, Is.True);
        using var assertContext = CreateContext();
        Assert.That(await assertContext.Orders.FindAsync(order.Id), Is.Null);
    }
}
