using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestTask.BLL.Response;

namespace TestTask.BLL.Interface
{
	public interface ILogBLL
	{
		Task<GetLogsResponse> GetAllAsync();
	}
}
