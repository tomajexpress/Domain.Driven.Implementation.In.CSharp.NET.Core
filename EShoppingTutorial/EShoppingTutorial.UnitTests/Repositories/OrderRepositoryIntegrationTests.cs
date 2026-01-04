namespace EShoppingTutorial.UnitTests.Repositories
{
    public class OrderRepositoryIntegrationTests
    {
        private DbContextOptionsBuilder<EShoppingTutorialDbContext> _builder;

        private EShoppingTutorialDbContext _dbContext;

        private OrderRepository _orderRepository;

        [OneTimeSetUp]
        public void Setup()
        {
            _builder = new DbContextOptionsBuilder<EShoppingTutorialDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString());

            _dbContext = new EShoppingTutorialDbContext(_builder.Options);
            _orderRepository = new OrderRepository(_dbContext);
        }

        [Test]
        public async Task MethodAdd_TrackingNumber_MustNotBeNull()
        {
            // arrange
            ProductId productId = new(1);
            var order = new Order(new CustomerId(1), "Germany", [new OrderItem (productId, new Price(amount: 2000, Currency.EUR))]);

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
