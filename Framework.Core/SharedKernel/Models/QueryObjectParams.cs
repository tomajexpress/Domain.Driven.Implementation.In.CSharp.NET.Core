using System.Collections.Generic;
namespace SharedKernel.Models;

public record QueryObjectParams : PageParam
{
    // Using an empty list as a default to prevent NullReferenceExceptions
    public List<SortParam> SortingParams { get; init; } = [];
}
