using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestTask.DAL.Context;
using TestTask.DAL.Interface;
using TestTask.DAL.Models;


namespace TestTask.DAL.Repository
{
	public class LogRepository : ILogRepository
	{
		private readonly UserContext _context;
		private readonly DbSet<Login> _logins;
		private readonly DbSet<User> _users;
		private readonly IMapper _mapper;

		public LogRepository(UserContext context, IMapper mapper)
		{
			_context = context;
			_logins = _context.Logins;
			_users = _context.Users;
			_mapper = mapper;
		}

		public async Task<List<LoginsUser>> GetAllAsync()
		{
			return await _logins
			.OrderByDescending(x => x.LoginTime)
			.Include(l => l.User) // Include the related User
			.Select(login => new LoginsUser
			{
				Id = login.Id,
				Username = login.User.Username, // Access the User's Username
				LoginTime = login.LoginTime,
				Success = login.Success,
				// Include other properties you need from the Login entity
			})
			.ToListAsync();
			}

		public async Task<Login> LogLoginAttempt(User user, bool success)
		{
			var foundUser = await _users.FirstOrDefaultAsync(x => x.Username == user.Username);
			if (foundUser != null)
			{
				var newLogin = new Login();
				newLogin.UserId = foundUser.Id;
				newLogin.Success = success;
				newLogin.LoginTime = DateTime.UtcNow;
				await _logins.AddAsync(newLogin);
				await _context.SaveChangesAsync();
				return newLogin;
			}
			return null;
		}
	}
}
