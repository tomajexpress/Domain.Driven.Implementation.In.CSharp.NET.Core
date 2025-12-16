using EShoppingTutorial.Core.Domain.Enums;

namespace EShoppingTutorialWebAPI.Models.OrderModels
{
    public class PriceSaveRequestModel
    {
        /// <example>100</example>
        public int? Amount { get; set; }

        /// <example>MoneyUnit.Euro</example>
        public MoneyUnit? Unit { get; set; } = MoneyUnit.UnSpecified;
    }
}
