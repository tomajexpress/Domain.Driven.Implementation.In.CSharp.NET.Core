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
            var order = new Order(new OrderItem[]
                                    {
                                        new OrderItem (3, new Price(2000, MoneyUnit.Euro))
                                    });

            // act

            _orderRepository.Add(order);

            var actualOrder = await _orderRepository.GetByIdAsync(1);

            // assert
            Assert.IsNotNull(actualOrder);

            Assert.IsNotNull(actualOrder.TrackingNumber);
        }


        [OneTimeTearDown]
        public void CleanUp()
        {
            _dbContext.Dispose();
        }
    }
}
