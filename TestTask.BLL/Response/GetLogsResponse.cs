﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestTask.BLL.DTO;

namespace TestTask.BLL.Response
{
	public class GetLogsResponse : BaseResponse
	{
		public List<LogDTO> Data { get; set; }
		public int Count { get; set; }
		public int PageNumber { get; set; }
		public int PageSize { get; set; }
		public int TotalCount { get; set; }
	}
}
