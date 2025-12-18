using NUnit.Framework;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EShoppingTutorial.Core.Persistence;
using EShoppingTutorial.Core.Domain.Entities;
using EShoppingTutorial.Core.Domain.ValueObjects;
using EShoppingTutorial.Core.Persistence.Repositories;
using EShoppingTutorial.Core.Domain.Enums;

namespace EShoppingTutorial.UnitTests.Repositories
{
    public class OrderRepositoryShould
    {
        private DbContextOptionsBuilder<EShoppingTutorialDbContext> _builder;

        private EShoppingTutorialDbContext _dbContext;

        private OrderRepository _orderRepository;

        [OneTimeSetUp]
        public void Setup()
        {
            _builder = new DbContextOptionsBuilder<EShoppingTutorialDbContext>()
                .UseInMemoryDatabase(databaseName: "Test_OrderRepository_Database");

            _dbContext = new EShoppingTutorialDbContext(_builder.Options);

            _orderRepository = new OrderRepository(_dbContext);
        }

        [Test]
        public async Task Test_MethodAdd_TrackingNumberMustNotBeNull_Ok()
        {
            // arrange
            ProductId productId = new(1);

            var order = new Order(new CustomerId(1), "Germany", [new OrderItem (productId, new Price(amount: 2000, MoneyUnit.Euro))]);

            // act

            _orderRepository.Add(order);

            var actualOrder = await _orderRepository.GetByIdAsync(order.Id);

            // assert
            Assert.That(actualOrder, Is.Not.Null);

            Assert.That(actualOrder.TrackingNumber, Is.Not.Null);
        }

        [OneTimeTearDown]
        public void CleanUp()
        {
            _dbContext.Dispose();
        }
    }
}
