using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TestTask.BLL.Interface;
using TestTask.BLL.Response;

namespace TestTask.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class LogController : ControllerBase
	{
		private readonly ILogBLL _logBLL;
		private readonly IMapper _mapper;

		public LogController(ILogBLL logBLL, IMapper mapper)
		{
			_logBLL = logBLL;
			_mapper = mapper;
		}

		[HttpGet, Authorize]
		public async Task<ActionResult<GetLogsResponse>> GetAllLogsAsync()
		{
			GetLogsResponse response = await _logBLL.GetAllAsync();
			if (!response.Success)
			{
				return BadRequest(response);
			}

			return Ok(response);
		}
	}
}
