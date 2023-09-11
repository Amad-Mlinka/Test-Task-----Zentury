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

		[HttpGet, Authorize]
		public async Task<ActionResult<GetUsersResponse>> GetAllUsers() {
			GetUsersResponse response = await _usersBLL.GetAllAsync();
			if (!response.Success)
			{
				return BadRequest(response);
			} 
			
			return Ok(response);

		}

		[HttpGet("{id}"), Authorize]
		public async Task<ActionResult<List<UserDTO>>> GetUserByID(int id)
		{
			var response = await _usersBLL.GetByIdAsync(id);
			if(response == null)
			{
				return BadRequest("No user by that ID");
			}
			return Ok(_mapper.Map<UserDTO>(response));
		}

		[HttpPost, Authorize]

		public async Task<ActionResult<AddUserResponse>> AddUser([FromBody] RegisterDTO user)
		{
			var response = await _usersBLL.AddAsync(user);
			if (!response.Success)
			{
				return BadRequest(response);
			}
			return Ok(response);
		}


		[HttpPut, Authorize]
		public async Task<ActionResult<EditUserResponse>> UpdateUser([FromBody] UpdateDTO user)
		{
			EditUserResponse response = await _usersBLL.UpdateAsync(user);
			if (!response.Success)
			{
				return BadRequest(response);
			}

			return Ok(response);
		}

		[HttpDelete("{id}"),Authorize]
		public async Task<ActionResult<DeleteUserResponse>> DeleteUser(int id)
		{
			DeleteUserResponse response = await _usersBLL.DeleteAsync(id);
			if (!response.Success)
			{
				return BadRequest(response);
			}

			return Ok(response);
		}

	}
}
