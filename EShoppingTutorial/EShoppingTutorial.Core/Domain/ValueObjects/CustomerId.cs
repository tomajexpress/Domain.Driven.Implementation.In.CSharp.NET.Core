using SharedKernel.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace EShoppingTutorial.Core.Domain.ValueObjects
{
    [NotMapped]
    public record CustomerId(int Value); 
}
