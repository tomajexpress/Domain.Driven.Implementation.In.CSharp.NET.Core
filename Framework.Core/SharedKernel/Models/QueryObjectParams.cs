namespace SharedKernel.Models;

public record QueryObjectParams : PageParam
{
    public List<SortParam> SortingParams { get; init; } = [];
}
