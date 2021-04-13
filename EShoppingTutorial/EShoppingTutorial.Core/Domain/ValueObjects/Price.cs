using EShoppingTutorial.Core.Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace EShoppingTutorial.Core.Domain.ValueObjects
{
    [ComplexType]
    public class Price
    {
        protected Price()
        {

        }

        public Price(int amount, MoneyUnit unit)
        {
            Amount = amount;

            Unit = unit;
        }


        public int Amount { get; protected set; }


        public MoneyUnit Unit { get; protected set; } = MoneyUnit.UnSpecified;


        public bool HasValue
        {
            get
            {
                return (Unit != MoneyUnit.UnSpecified);
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
