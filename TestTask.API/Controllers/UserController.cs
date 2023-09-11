using Microsoft.AspNetCore.Mvc;
using TestTask.BLL.Interface;
using TestTask.BLL.DTO;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using TestTask.BLL.Response;

namespace TestTask.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UserController : ControllerBase
	{
		private readonly IBLL _usersBLL;
		private readonly IMapper _mapper;

		public UserController(IBLL usersBLL, IMapper mapper)
		{
			_usersBLL = usersBLL;
			_mapper = mapper;
		}

		[HttpGet, Authorize]
		[Route("all")]
		public async Task<ActionResult<GetAllUsersResponse>> GetAllUsers() {
			GetAllUsersResponse response = await _usersBLL.GetAllAsync();

			
			return Ok(response);
		}

		[HttpGet]
		public async Task<ActionResult<GetUsersResponse>> GetUsers([FromQuery] int pageNumber, int pageSize)
		{
			GetUsersResponse response = await _usersBLL.GetAsync(pageNumber, pageSize);


			return Ok(response);

		}

		[HttpPost, Authorize]

		public async Task<ActionResult<AddUserResponse>> AddUser([FromBody] RegisterDTO user)
		{
			AddUserResponse response = await _usersBLL.AddAsync(user);

			return Ok(response);
		}


		[HttpPut, Authorize]
		public async Task<ActionResult<EditUserResponse>> UpdateUser([FromBody] UpdateDTO user)
		{
			EditUserResponse response = await _usersBLL.UpdateAsync(user);

			return Ok(response);
		}

		[HttpDelete("{id}"),Authorize]
		public async Task<ActionResult<DeleteUserResponse>> DeleteUser(int id)
		{
			DeleteUserResponse response = await _usersBLL.DeleteAsync(id);

			return Ok(response);
		}

	}
}
