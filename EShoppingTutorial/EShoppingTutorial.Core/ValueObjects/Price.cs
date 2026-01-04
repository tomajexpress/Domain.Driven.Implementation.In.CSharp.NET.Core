namespace EShoppingTutorial.Core.Domain.ValueObjects;

public record Price
{
    public decimal Amount { get; init; }
    public Currency Currency { get; init; } = Currency.Unspecified;

    // EF Core requires a parameterless constructor
    protected Price() { }

    public Price(decimal amount, Currency currency)
    {
        if (Currency.Unspecified == currency)
        {
            throw new BusinessRuleBrokenException("You must supply a valid money currency!");
        }

        if (amount < 0)
        {
            throw new BusinessRuleBrokenException("Price amount cannot be negative.");
        }

        Amount = amount;
        Currency = currency;
    }

    public bool HasValue => Currency != Currency.Unspecified && Amount >= 0;

    public static Price operator +(Price left, Price right)
    {
        if (left.Currency != right.Currency)
            throw new BusinessRuleBrokenException("Cannot add different currencies.");

        return new Price(left.Amount + right.Amount, left.Currency);
    }

    public static Price operator -(Price left, Price right)
    {
        if (left.Currency != right.Currency)
            throw new BusinessRuleBrokenException("Cannot subtract different currencies.");

        return new Price(left.Amount - right.Amount, left.Currency);
    }

    public override string ToString() =>
        Currency != Currency.Unspecified ?
        Amount + " " + MoneySymbols.GetSymbol(Currency) :
        Amount.ToString();
}