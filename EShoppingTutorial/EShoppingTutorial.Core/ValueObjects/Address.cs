namespace EShoppingTutorial.Core.Domain.ValueObjects;

public record Address
{
    public string Street { get; init; } = default!;
    public string City { get; init; } = default!;
    public string Country { get; init; } = default!;
    public string ZipCode { get; init; } = default!;

    // EF Core requires a parameterless constructor
    protected Address() { }

    public Address(string street, string city, string country, string zipCode)
    {
        if (string.IsNullOrWhiteSpace(street)) throw new BusinessRuleBrokenException("Street is required.");
        if (string.IsNullOrWhiteSpace(city)) throw new BusinessRuleBrokenException("City is required.");
        if (string.IsNullOrWhiteSpace(country)) throw new BusinessRuleBrokenException("Country is required.");
        if (string.IsNullOrWhiteSpace(zipCode)) throw new BusinessRuleBrokenException("ZipCode is required.");

        Street = street;
        City = city;
        Country = country;
        ZipCode = zipCode;
    }

    public override string ToString() => $"{Street}, {City}, {ZipCode}, {Country}";
}