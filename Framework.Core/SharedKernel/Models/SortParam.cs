namespace SharedKernel.Models;

public class SortParam
{
    public bool? SortOrderDescending { get; set; }
    
    public required string OrderProperty { get; set; }
}
