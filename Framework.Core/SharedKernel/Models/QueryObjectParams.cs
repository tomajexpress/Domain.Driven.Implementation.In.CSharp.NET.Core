using System.Collections.Generic;

namespace SharedKernel.Models
{
	public class QueryObjectParams : PageParam
	{
        public List<SortParam> SortingParams { get; set; }
    }
}
