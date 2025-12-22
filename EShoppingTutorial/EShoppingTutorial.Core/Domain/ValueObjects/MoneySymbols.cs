using System.Collections.Generic;
using EShoppingTutorial.Core.Domain.Enums;

namespace EShoppingTutorial.Core.Domain.ValueObjects
{
    public static class MoneySymbols
    {
        private static Dictionary<MoneyUnit, string> _symbols;

        static MoneySymbols()
        {
            if (_symbols != null)
                return;

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
}
