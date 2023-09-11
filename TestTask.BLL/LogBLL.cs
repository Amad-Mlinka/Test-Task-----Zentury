using AutoMapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestTask.BLL.DTO;
using TestTask.BLL.Interface;
using TestTask.BLL.Response;
using TestTask.DAL.Interface;

namespace TestTask.BLL
{
	public class LogBLL:ILogBLL
	{
		private readonly ILogRepository _repository;
		private readonly IMapper _mapper;
		private readonly IConfiguration _configuration;

		public LogBLL(ILogRepository repository, IMapper mapper, IConfiguration configuration)
		{
			_repository = repository;
			_mapper = mapper;
			_configuration = configuration;
		}

		public async Task<GetAllLogsResponse> GetAllAsync()
		{
			GetAllLogsResponse response = new();

			try
			{
				response.Success = true;
				response.Data = _mapper.Map<List<DTO.LogDTO>>(await _repository.GetAllAsync());
				return response;

			}
			catch (Exception ex)
			{
				response.Error=ex.Message;
				response.Success=false;
				return response;
			}
		}

		public async Task<GetLogsResponse> GetAsync(int pageNumber, int pageSize)
		{
			GetLogsResponse response = new GetLogsResponse();
			try
			{
				var data = await _repository.GetAsync(pageNumber, pageSize);
				response.Data = _mapper.Map<List<LogDTO>>(data.Data);
				response.Success = true;
				response.TotalCount = data.TotalCount;
				response.Count = data.Count;
				response.PageNumber = pageNumber;
				response.PageSize = pageSize;
				return response;
			}
			catch (Exception ex)
			{
				response.Success = false;
				response.Error = ex.Message;
				response.Message = "";
				response.Data = null;

				return response;
			}
		}



	}
}
