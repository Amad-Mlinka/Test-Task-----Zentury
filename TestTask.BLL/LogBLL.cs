using AutoMapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

		public async Task<GetLogsResponse> GetAllAsync()
		{
			GetLogsResponse response = new();

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


	}
}
