using NUnit.Framework;
using EShoppingTutorial.Core.Domain.ValueObjects;

namespace EShoppingTutorial.UnitTests.Domain.ValueObjects;

[TestFixture]
public class MoneySymbolUnitTests
{
    [Test]
    public void GetSymbol_ForSpecifiedType_MustReturnCorrectValue()
    {
        // act
        var actualResult = MoneySymbols.GetSymbol(Core.Domain.Enums.MoneyUnit.USD);

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
