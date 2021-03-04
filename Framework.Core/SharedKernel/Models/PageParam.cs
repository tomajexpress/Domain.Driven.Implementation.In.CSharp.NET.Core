namespace SharedKernel.Models
{
    public class PageParam
	{
		const int maxPageSize = 50;

		private int _pageSize = 10;

		/// <summary>
		/// اندازه صفحه
		/// </summary>
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

		/// <summary>
		/// شماره صفحه
		/// </summary>
		public int PageNumber { get; set; } = 1;
	}
}
