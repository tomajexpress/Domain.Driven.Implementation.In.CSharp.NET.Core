using EShoppingTutorial.Core.Domain.Enums;
using SharedKernel.Exceptions;

namespace EShoppingTutorial.Core.Domain.ValueObjects
{
    public record Price
    {
        protected Price() // For Entity Framework Core
        {

        }

        public Price(decimal amount, MoneyUnit unit)
        {
            if (MoneyUnit.UnSpecified == unit)
            {
                throw new BusinessRuleBrokenException("You must supply a valid money unit!");
            }

            Amount = amount;
            Unit = unit;
        }

        public decimal Amount { get; protected set; }

        public MoneyUnit Unit { get; protected set; } = MoneyUnit.UnSpecified;

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
}
