namespace SharedKernel.Models
{
    /// <summary>
    /// مرتب سازی
    /// </summary>
    public class SortParam
    {
        /// <summary>
        /// نزولی بودن
        /// </summary>
        public bool? SortOrderDescending { get; set; }
        
        /// <summary>
        /// فیلد مرتب سازی 
        /// </summary>
        public string OrderProperty { get; set; }
    }
}
