using EShoppingTutorial.Core.Domain.Enums;
using EShoppingTutorial.Core.Domain.Services;
using EShoppingTutorial.Core.Domain.ValueObjects;

namespace EShoppingTutorial.Infrastructure.ExternalServices;

public class ExternalTaxProvider : ITaxCalculationService
{
    public async Task<Price> CalculateTaxAsync(Address shippingAddress, decimal orderTotal, Currency currency)
    {
        // In a real scenario, this would call an API like TaxJar or Avalara
        // var response = await _httpClient.GetFromJsonAsync<TaxResponse>(...);
        // For this example, we will mock the tax calculation

        decimal mockTaxRate = shippingAddress.Country.Equals("USA") ? 0.08m : 0.15m;

        var amount = orderTotal * mockTaxRate;

        return new Price(amount, currency);
    }
}