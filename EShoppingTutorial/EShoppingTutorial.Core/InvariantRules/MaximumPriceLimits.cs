namespace EShoppingTutorial.Core.Domain.InvariantRules;

public static class MaximumPriceLimits
{
    public static decimal GetMaximumPriceLimit(Currency curency) => curency switch
    {
        Currency.USD => 10_000m,
        Currency.EUR => 9_000m,
        Currency.Rial => 8_000m,
        _ => throw new BusinessRuleBrokenException("MoneyUnit (Currency) is not valid!")
    };
}