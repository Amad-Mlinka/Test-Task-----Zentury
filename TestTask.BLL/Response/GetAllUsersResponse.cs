using TestTask.BLL.DTO;

namespace TestTask.BLL.Response
{
	public class GetAllUsersResponse : BaseResponse
	{
		public List<UserDTO> Data { get; set; }
	}
}
