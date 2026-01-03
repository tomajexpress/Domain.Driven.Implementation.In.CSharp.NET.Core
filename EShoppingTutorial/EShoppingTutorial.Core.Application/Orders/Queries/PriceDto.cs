namespace EShoppingTutorial.Core.Application.Orders.Queries;

public record PriceDto
{
    public decimal Amount { get; set; }
    public required string Currency { get; set; }
}