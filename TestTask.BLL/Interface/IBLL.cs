using TestTask.BLL.DTO;
using TestTask.BLL.Response;

namespace TestTask.BLL.Interface
{
	public interface IBLL
	{
		Task<GetUsersResponse> GetAllAsync();
		Task<AddUserResponse> AddAsync(RegisterDTO user);
		Task<UserDTO> GetByIdAsync(int id);
		Task<EditUserResponse> UpdateAsync(RegisterDTO item);
		Task<DeleteUserResponse> DeleteAsync(int id);
	}
}
