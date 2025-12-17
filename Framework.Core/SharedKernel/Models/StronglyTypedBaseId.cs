using System.ComponentModel.DataAnnotations.Schema;

namespace SharedKernel.Models
{
    [NotMapped]
    public record StronglyTypedBaseId(int Value);
}
