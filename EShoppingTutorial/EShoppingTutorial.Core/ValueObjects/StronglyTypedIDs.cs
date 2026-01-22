using System.ComponentModel.DataAnnotations.Schema;

namespace EShoppingTutorial.Core.Domain.ValueObjects;

[NotMapped]
public sealed record OrderId(int Value);

[NotMapped]
public sealed record OrderItemId(int Value);

[NotMapped]
public sealed record ProductId(int Value);

[NotMapped]
public sealed record CustomerId(int Value);
