using EShoppingTutorial.Core.Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace EShoppingTutorial.Core.Domain.ValueObjects
{
    [ComplexType]
    public class Price
    {
        public int Amount { get; set; }

        public MoneyUnit Unit { get; set; } = MoneyUnit.UnSpecified;

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
