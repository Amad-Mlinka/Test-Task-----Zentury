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
		Task<Login> LogLoginAttempt(User user, bool success);
	}
}
