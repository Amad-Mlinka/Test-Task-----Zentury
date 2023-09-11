using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestTask.DAL.Models
{
	public class LoginsUser
	{
		public int Id { get; set; }
		public string Username { get; set; }
		public DateTime LoginTime { get; set; }
		public bool Success { get; set; }
	}
}
