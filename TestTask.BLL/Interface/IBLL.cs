using TestTask.BLL.DTO;
using TestTask.BLL.Response;

namespace TestTask.BLL.Interface
{
	public interface IBLL
	{
		Task<GetAllUsersResponse> GetAllAsync();
		Task<GetUsersResponse> GetAsync(int pageSize, int pageNumber);
		Task<AddUserResponse> AddAsync(RegisterDTO user);
		Task<UserDTO> GetByIdAsync(int id);
		Task<EditUserResponse> UpdateAsync(RegisterDTO item);
		Task<DeleteUserResponse> DeleteAsync(int id);
	}
}
