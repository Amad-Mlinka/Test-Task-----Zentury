using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestTask.DAL.Models;

namespace TestTask.DAL.Interface
{
	public interface ILogRepository
	{
		Task<List<LoginsUser>> GetAllAsync();
		Task<GetLogResponse> GetAsync(int pageNumber, int pageSize=10);
		Task<Login> LogLoginAttempt(User user, bool success);
	}
}
