using TestTask.BLL.DTO;
using TestTask.BLL.Response;

namespace TestTask.BLL.Interface
{
	public interface IAuthBLL
	{
		Task<AuthResponse> RegisterUser(RegisterDTO request);
		Task<LoginResponse> LoginUser(LoginDTO request);

	}
}
