using NUnit.Framework;
using EShoppingTutorial.Core.Domain.ValueObjects;

namespace EShoppingTutorial.UnitTests.Domain.ValueObjects
{
    public class MoneySymbolShould
    {
        [Test]
        public void GetSymbol_ForSpecifiedType_MustReturnCorrectValue()
        {
            // act
            var actualResult = MoneySymbols.GetSymbol(Core.Domain.Enums.MoneyUnit.Dollar);

            // assert
            Assert.That(actualResult.Equals("$"));
        }

        [Test]
        public void GetSymbol_For_UnSpecifiedType_MustReturnEmptyString()
        {
            // act
            var actualResult = MoneySymbols.GetSymbol(Core.Domain.Enums.MoneyUnit.UnSpecified);

            // assert
            Assert.That(actualResult.Equals(string.Empty));
        }
    }
}
