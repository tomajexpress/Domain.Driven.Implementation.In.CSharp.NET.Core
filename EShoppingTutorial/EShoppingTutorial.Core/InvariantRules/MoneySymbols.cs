namespace EShoppingTutorial.Core.Domain.InvariantRules;

public static class MoneySymbols
{
    private static readonly Dictionary<Currency, string> _symbols;

    static MoneySymbols()
    {
        if (_symbols != null) return;

        _symbols = new Dictionary<Currency, string>
        {
            { Currency.Unspecified, string.Empty },

            { Currency.USD, "$" },

            { Currency.EUR, "€" },

            { Currency.Rial, "Rial" },
        };
    }

    public static string GetSymbol(Currency currency)
    {
        return _symbols[currency].ToString();
    }
}
