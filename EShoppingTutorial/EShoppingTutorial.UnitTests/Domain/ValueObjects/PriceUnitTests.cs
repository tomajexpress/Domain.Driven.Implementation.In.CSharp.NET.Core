namespace EShoppingTutorial.UnitTests.Domain.ValueObjects;

[TestFixture]
public class PriceUnitTests
{
    [Test]
    public void ToString_ForSpecifiedUnitType_MustReturnCorrectValue()
    {
        // Arrange
        var price = new Price(14, Currency.USD);

        // Act
        var actualResult = price.ToString();

        // Assert
        Assert.That(actualResult.Equals("14 $"));
    }

    [Test]
    public void PlusOperator_ShouldReturnCorrectSum_WhenCurrenciesMatch()
    {
        // Arrange
        var price1 = new Price(100, Currency.USD);
        var price2 = new Price(50, Currency.USD);

        // Act
        var result = price1 + price2;

        // Assert
        Assert.That(result.Amount, Is.EqualTo(150));
        Assert.That(result.Currency, Is.EqualTo(Currency.USD));
    }

    [Test]
    public void PlusOperator_ShouldThrowException_WhenCurrenciesDoNotMatch()
    {
        // Arrange
        var priceUsd = new Price(100, Currency.USD);
        var priceEur = new Price(100, Currency.EUR);

        // Act & Assert
        var ex = Assert.Throws<BusinessRuleBrokenException>(() =>
        {
            var _ = priceUsd + priceEur;
        });

        Assert.That(ex.Message, Is.EqualTo("Cannot add different currencies."));
    }

    [Test]
    public void MinusOperator_ShouldReturnCorrectDifference_WhenCurrenciesMatch()
    {
        // Arrange
        var price1 = new Price(100, Currency.USD);
        var price2 = new Price(30, Currency.USD);

        // Act
        var result = price1 - price2;

        // Assert
        Assert.That(result.Amount, Is.EqualTo(70));
        Assert.That(result.Currency, Is.EqualTo(Currency.USD));
    }

    [TestCase(-1)]
    [TestCase(-100)]
    public void Constructor_ShouldThrowException_WhenAmountIsNegative(decimal negativeAmount)
    {
        // Act & Assert
        var ex = Assert.Throws<BusinessRuleBrokenException>(() => new Price(negativeAmount, Currency.USD));

        Assert.That(ex.Message, Is.EqualTo("Price amount cannot be negative."));
    }
}
