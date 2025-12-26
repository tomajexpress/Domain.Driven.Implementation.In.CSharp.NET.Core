namespace EShoppingTutorial.Core.Domain.InvariantRules;

public static class MaximumPriceLimits
{
    public static decimal GetMaximumPriceLimit(MoneyUnit unit) => unit switch
    {
        MoneyUnit.USD => 10_000m,
        MoneyUnit.EUR => 9_000m,
        MoneyUnit.Rial => 8_000m,
        _ => throw new BusinessRuleBrokenException("MoneyUnit (Currency) is not valid!")
    };
}