using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestTask.DAL.Models
{
	public class GetLogResponse
	{
		public List<LoginsUser> Data { get; set; }
		public int Count { get; set; }
		public int PageNumber { get; set; }
		public int PageSize { get; set; }
		public int TotalCount { get; set; }
	}
}
