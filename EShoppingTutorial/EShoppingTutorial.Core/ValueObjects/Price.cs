namespace EShoppingTutorial.Core.Domain.ValueObjects;

public record Price
{
    public decimal Value { get; init; }
    public Currency Currency { get; init; } = Currency.Unspecified;

    // EF Core requires a parameterless constructor
    protected Price() { }

    public Price(decimal value, Currency currency)
    {
        if (Currency.Unspecified == currency)
        {
            throw new BusinessRuleBrokenException("You must supply a valid money currency!");
        }

        if (value < 0)
        {
            throw new BusinessRuleBrokenException("Price value cannot be negative.");
        }

        Value = value;
        Currency = currency;
    }

    public bool HasValue => Currency != Currency.Unspecified && Value >= 0;

    public static Price operator +(Price left, Price right)
    {
        if (left.Currency != right.Currency)
            throw new BusinessRuleBrokenException("Cannot add different currencies.");

        return new Price(left.Value + right.Value, left.Currency);
    }

    public static Price operator -(Price left, Price right)
    {
        if (left.Currency != right.Currency)
            throw new BusinessRuleBrokenException("Cannot subtract different currencies.");

        return new Price(left.Value - right.Value, left.Currency);
    }

    public override string ToString() =>
        Currency != Currency.Unspecified ?
        Value + " " + MoneySymbols.GetSymbol(Currency) :
        Value.ToString();
}