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
	public class UserRepository : IUserRepository
	{
		private readonly UserContext _context;
		private readonly DbSet<User> _users;
		private readonly IMapper _mapper;

		public UserRepository(UserContext context, IMapper mapper)
		{
			_context = context;
			_users = _context.Users;
			_mapper = mapper;
		}

		public async Task<List<User>> GetAllAsync()
		{
			return await _users.OrderByDescending(x=>x.Username).ToListAsync();
		}

		public async Task<User> GetByIdAsync(int id)
		{
			return await _users.FindAsync(id);
		}

		public async Task<User> GetByUsernameAsync(string username)
		{
			return await _users.FirstOrDefaultAsync(x=>x.Username == username);
		}

		public async Task<User> GetByEmailAsync(string email)
		{
			return await _users.FirstOrDefaultAsync(x => x.Email == email);
		}

		public async Task<User> AddAsync(User user)
		{
			await _users.AddAsync(user);
			await _context.SaveChangesAsync();

			return user;
		}

		public async Task<User> UpdateAsync(User user)
		{
			var foundUser = await GetByUsernameAsync(user.Username);
			if (foundUser == null)
			{
				return null;
			}
			_mapper.Map(user, foundUser);
			await _context.SaveChangesAsync();
			return foundUser;
		}

		public async Task UpdateTokenAsync(User user)
		{
			var foundUser = await _users.FirstOrDefaultAsync(x => x.Username == user.Username);
			if (foundUser == null)
			{
				return;
			}
			foundUser.TokenCreated = user.TokenCreated;
			foundUser.TokenExpires = user.TokenExpires;
			await _context.SaveChangesAsync();
		}

		public async Task<User> DeleteAsync(int id)
		{
			var user = await _users.FindAsync(id);
			if (user != null)
			{
				User deletedUser = new(); 
				_mapper.Map(user, deletedUser); 
				_users.Remove(user);
				await _context.SaveChangesAsync();
				return deletedUser;
			}
			return null;
		}

		
	}
}
