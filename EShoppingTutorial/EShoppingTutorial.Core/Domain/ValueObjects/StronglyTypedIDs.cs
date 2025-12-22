using System.ComponentModel.DataAnnotations.Schema;

namespace EShoppingTutorial.Core.Domain.ValueObjects;

[NotMapped]
public record OrderId(int Value);

[NotMapped]
public record OrderItemId(int Value);

[NotMapped]
public record ProductId(int Value);

[NotMapped]
public record CustomerId(int Value);
