namespace SharedKernel.Models;

public record SortParam
{
    public bool? SortOrderDescending { get; set; }
    
    public required string OrderProperty { get; set; }
}
