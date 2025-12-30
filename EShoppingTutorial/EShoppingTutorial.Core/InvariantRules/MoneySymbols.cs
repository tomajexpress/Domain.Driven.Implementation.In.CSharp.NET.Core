namespace EShoppingTutorial.Core.Domain.InvariantRules;

public static class MoneySymbols
{
    private static readonly Dictionary<MoneyUnit, string> _symbols;

    static MoneySymbols()
    {
        if (_symbols != null) return;

        _symbols = new Dictionary<MoneyUnit, string>
        {
            { MoneyUnit.UnSpecified, string.Empty },

            { MoneyUnit.USD, "$" },

            { MoneyUnit.EUR, "€" },

            { MoneyUnit.Rial, "Rial" },
        };
    }

    public static string GetSymbol(MoneyUnit moneyUnit)
    {
        return _symbols[moneyUnit].ToString();
    }
}
