using System.Collections.Generic;

namespace SharedKernel.Models
{
	/// <summary>
	/// داده مدل برای صفحه بندی و مرتب سازی
	/// </summary>
	public class QueryObjectParams : PageParam
	{
        /// <summary>
        /// داده های مرتب سازی
        /// </summary>
        public List<SortParam> SortingParams { get; set; }
    }
}
