using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestTask.BLL.DTO
{
	public class LoginDTO
	{
		public string Username { get; set; }
		public string Password { get; set; }
		public bool RememberMe { get; set; }
	}
}
