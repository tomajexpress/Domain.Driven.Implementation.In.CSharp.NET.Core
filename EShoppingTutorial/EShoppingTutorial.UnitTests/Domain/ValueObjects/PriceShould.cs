using EShoppingTutorial.Core.Domain.Enums;
using EShoppingTutorial.Core.Domain.ValueObjects;
using NUnit.Framework;

namespace EShoppingTutorial.UnitTests.Domain.ValueObjects
{
    public class PriceShould
    {
        [Test]
        public void ToString_ForSpecifiedUnitType_MustReturnCorrectValue()
        {
            // arrange
            var price = new Price(14, MoneyUnit.Dollar);

            // act
            var actualResult = price.ToString();
            
            // assert
            Assert.That(actualResult.Equals("14 $"));
        }
    }
}
