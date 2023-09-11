using Microsoft.AspNetCore.Mvc;
using TestTask.BLL.DTO;
using TestTask.BLL.Interface;
using TestTask.BLL.Response;

namespace TestTask.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuthController : ControllerBase
	{
		private readonly IConfiguration _configuration;
		private readonly IAuthBLL _authBLL;

		public AuthController(IConfiguration configuration, IAuthBLL authBLL)
		{
			_configuration = configuration;
			_authBLL = authBLL;
		}


		[HttpPost("register")]
		public async Task<ActionResult<AuthResponse>> Register(RegisterDTO request)
		{
			var response = await _authBLL.RegisterUser(request);


			return Ok(response);
		}

		[HttpPost("login")]
		public async Task<ActionResult<LoginResponse>> Login(LoginDTO request)
		{
			var response = await _authBLL.LoginUser(request);


			return Ok(response);
		}




	}
}
