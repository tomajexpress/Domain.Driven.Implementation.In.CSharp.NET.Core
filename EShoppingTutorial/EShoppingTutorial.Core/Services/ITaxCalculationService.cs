namespace EShoppingTutorial.Core.Domain.Services;

public interface ITaxCalculationService
{
    // We return a Task because external lookups (APIs/DBs) are asynchronous
    Task<Price> CalculateTaxAsync(Address shippingAddress, decimal orderTotal, Currency currency);
}