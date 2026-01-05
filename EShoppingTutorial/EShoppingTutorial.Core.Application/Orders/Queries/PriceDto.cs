namespace EShoppingTutorial.Core.Application.Orders.Queries;

public record PriceDto
{
    public decimal Value { get; set; }
    public required string Currency { get; set; }
}