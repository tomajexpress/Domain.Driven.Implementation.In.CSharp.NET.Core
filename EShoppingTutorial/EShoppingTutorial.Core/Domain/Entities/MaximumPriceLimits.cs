using EShoppingTutorial.Core.Domain.Enums;
using SharedKernel.Exceptions;
using System.Collections.Generic;

namespace EShoppingTutorial.Core.Domain.Entities
{
    public static class MaximumPriceLimits
    {
        private static Dictionary<MoneyUnit, int> _maximumPriceLimits;

        static MaximumPriceLimits()
        {
            if (_maximumPriceLimits != null)
                return;

            _maximumPriceLimits = new Dictionary<MoneyUnit, int>
            {
                { MoneyUnit.Euro, 9000 },

                { MoneyUnit.Dollar, 10000 },

                { MoneyUnit.Rial, 12000 },
            };
        }

        public static int GetMaximumPriceLimit(MoneyUnit unit)
        {
            if (unit == MoneyUnit.UnSpecified)
            {
                throw new BusinessRuleBrokenException("Money Unit is not defined !");
            }

            return _maximumPriceLimits[unit];
        }
    }
}
