using System.ComponentModel.DataAnnotations.Schema;

namespace EShoppingTutorial.Core.Domain.ValueObjects
{
    [NotMapped]
    public record ProductId(int Value);
}
