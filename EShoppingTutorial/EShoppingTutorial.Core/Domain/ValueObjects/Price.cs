using EShoppingTutorial.Core.Domain.Enums;
using SharedKernel.Exceptions;
using System;

namespace EShoppingTutorial.Core.Domain.ValueObjects;

public record Price
{
    public decimal Amount { get; protected set; }
    public MoneyUnit Unit { get; protected set; } = MoneyUnit.UnSpecified;

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
            throw new ArgumentException("Price amount cannot be negative.");
        }

        Amount = amount;
        Unit = unit;
    }

    public static Price Create(decimal amount, MoneyUnit unit) => new(amount, unit);

    public bool HasValue
    {
        get
        {
            return Unit != MoneyUnit.UnSpecified && Amount != 0;
        }
    }

    public override string ToString()
    {
        return 
            Unit != MoneyUnit.UnSpecified ? 
            Amount + " " + MoneySymbols.GetSymbol(Unit) : 
            Amount.ToString();
    }
}
