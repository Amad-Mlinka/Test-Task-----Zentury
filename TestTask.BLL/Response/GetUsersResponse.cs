using TestTask.BLL.DTO;

namespace TestTask.BLL.Response
{
	public class GetUsersResponse : BaseResponse
	{
		public List<UserDTO> Data { get; set; }
	}
}
