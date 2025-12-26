namespace EShoppingTutorial.Core.Domain.Entities;

public class OrderItem
{
    public OrderItemId Id { get; protected set; }
    public ProductId ProductId { get; protected set; }
    public Price Price { get; protected set; }
    public OrderId OrderId { get; protected set; }

    // EF Core requires a parameterless constructor
    protected OrderItem() { }

    public OrderItem(ProductId productId, Price price)
    {
        ProductId = productId ?? throw new ArgumentNullException(nameof(productId));
        Price = price ?? throw new ArgumentNullException(nameof(price));
        CheckForBrokenRules();
    }

    private void CheckForBrokenRules()
    {
        if (ProductId.Value <= 0)
        {
            throw new BusinessRuleBrokenException("You must supply valid Product!");
        }

        if (!Price.HasValue)
        {
            throw new BusinessRuleBrokenException("You must supply valid Price!");
        }
    }
}
