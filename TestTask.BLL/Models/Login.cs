using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestTask.BLL.Models
{
	public class Login
	{
		public int Id { get; set; }
		public DateTime LoginTime { get; set; }
		public bool Success { get; set; }
		public int UserId { get; set; }
	}
}
