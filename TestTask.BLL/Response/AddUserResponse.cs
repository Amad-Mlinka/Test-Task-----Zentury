using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestTask.BLL.DTO;

namespace TestTask.BLL.Response
{
	public class AddUserResponse : BaseResponse
	{
		public UserDTO Data {  get; set; }
	}
}
