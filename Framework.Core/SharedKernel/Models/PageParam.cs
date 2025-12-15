namespace SharedKernel.Models
{
    public class PageParam
	{
		const int maxPageSize = 50;

		private int _pageSize = 10;

		public int PageSize
		{
			get
			{
				return _pageSize;
			}
			set
			{
				_pageSize = (value > maxPageSize) ? maxPageSize : value;
			}
		}

		public int PageNumber { get; set; } = 1;
	}
}
