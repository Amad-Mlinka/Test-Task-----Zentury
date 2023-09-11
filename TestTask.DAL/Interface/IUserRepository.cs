using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestTask.DAL.Models;

namespace TestTask.DAL.Interface
{
	public interface IUserRepository
	{
		Task<List<User>> GetAllAsync();
		Task<User> GetByIdAsync(int id);
		Task<User> AddAsync(User entity);
		Task<User> UpdateAsync(User entity);
		Task<User> DeleteAsync(int id);
		Task<User> GetByUsernameAsync(string username);
		Task<User> GetByEmailAsync(string username);
		Task UpdateTokenAsync(User user);


	}

}
