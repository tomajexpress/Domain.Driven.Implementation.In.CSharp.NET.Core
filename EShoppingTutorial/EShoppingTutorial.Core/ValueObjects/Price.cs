namespace EShoppingTutorial.Core.Domain.ValueObjects;

public record Price
{
    public decimal Amount { get; init; }
    public MoneyUnit Unit { get; init; } = MoneyUnit.UnSpecified;

    // EF Core requires a parameterless constructor
    protected Price() { }

    public Price(decimal amount, MoneyUnit unit)
    {
        if (MoneyUnit.UnSpecified == unit)
        {
            throw new BusinessRuleBrokenException("You must supply a valid money unit!");
        }

        if (amount < 0)
        {
            throw new BusinessRuleBrokenException("Price amount cannot be negative.");
        }

        Amount = amount;
        Unit = unit;
    }

    public bool HasValue => Unit != MoneyUnit.UnSpecified && Amount >= 0;

    public override string ToString() =>
        Unit != MoneyUnit.UnSpecified ?
        Amount + " " + MoneySymbols.GetSymbol(Unit) :
        Amount.ToString();
}