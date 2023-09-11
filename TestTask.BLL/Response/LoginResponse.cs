using TestTask.BLL.Models;

namespace TestTask.BLL.Response
{
	public class LoginResponse : AuthResponse
	{
		public string Token { get; set; }
		public RefreshToken RefreshToken { get; set; }
	}
}
