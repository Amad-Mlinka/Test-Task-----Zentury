using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Editing;
using TestTask.BLL.Interface;
using TestTask.BLL.Models;
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

		
		[HttpGet("{pageSize}/{pageNumber}")]
		public async Task<ActionResult<GetUsersResponse>> GetUsers(int pageSize,int pageNumber)
		{
			GetUsersResponse response = await _usersBLL.GetAsync(pageSize,pageNumber);


			return Ok(response);

		}

		[HttpGet("{id}"), Authorize]
		public async Task<ActionResult<List<UserDTO>>> GetUserByID(int id)
		{
			var response = await _usersBLL.GetByIdAsync(id);

			return Ok(_mapper.Map<UserDTO>(response));
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
