using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestTask.DAL.Models
{
	public class Login
	{
		public int Id { get; set; }
		public DateTime LoginTime { get; set; }
		public bool Success { get; set; }

		// Foreign key property
		public int UserId { get; set; }
		// Navigation property for the related User
		public User User { get; set; }
	}
}
