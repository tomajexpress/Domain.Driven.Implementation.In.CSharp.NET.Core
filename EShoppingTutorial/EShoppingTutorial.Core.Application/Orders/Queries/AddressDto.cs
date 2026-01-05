namespace EShoppingTutorial.Core.Application.Orders.Queries;
public record AddressDto(
    string Street,
    string City,
    string Country,
    string ZipCode);
