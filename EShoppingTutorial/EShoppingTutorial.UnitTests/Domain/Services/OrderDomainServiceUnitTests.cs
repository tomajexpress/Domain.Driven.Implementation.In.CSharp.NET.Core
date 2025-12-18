using EShoppingTutorial.Core.Domain;
using EShoppingTutorial.Core.Domain.Entities;
using EShoppingTutorial.Core.Domain.Repositories;
using EShoppingTutorial.Core.Domain.Services;
using EShoppingTutorial.Core.Domain.ValueObjects;
using Moq;
using NUnit.Framework;
using System.Threading.Tasks;

namespace EShoppingTutorial.UnitTests.Domain.Services
{
    public class OrderDomainServiceUnitTests
    {
        [Test]
        public async Task DeleteOrderAsync_WhenOrderDoesNotExist_ShouldReturnFalseAndNotCommit()
        {
            // Arrange
            var mockRepo = new Mock<IOrderRepository>();
            var mockUow = new Mock<IUnitOfWork>();
            mockRepo.Setup(x => x.GetByIdAsync(It.IsAny<OrderId>())).ReturnsAsync((Order)null);
            mockUow.Setup(x => x.OrderRepository).Returns(mockRepo.Object);

            var service = new OrderDomainService(mockUow.Object);

            // Act
            var result = await service.DeleteOrderAsync(new OrderId(1));

            // Assert
            Assert.That(result, Is.False);
            mockUow.Verify(x => x.CompleteAsync(), Times.Never); 
        }
    }
}
