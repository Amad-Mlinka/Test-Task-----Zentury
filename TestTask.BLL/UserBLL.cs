using AutoMapper;
using TestTask.BLL.Interface;
using TestTask.DAL.Interface;
using TestTask.BLL.DTO;
using System.Security.Cryptography;
using TestTask.BLL.Response;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Text.RegularExpressions;

namespace TestTask.BLL
{
	public class UserBLL : IBLL
	{
		private readonly IUserRepository _repository; // Use the generic IUserRepository<User>
		private readonly IMapper _mapper;

		public UserBLL(IUserRepository repository, IMapper mapper) // Inject the generic IUserRepository<User>
		{
			_repository = repository;
			_mapper = mapper;
		}
		#region Public Methods
		public async Task<GetUsersResponse> GetAllAsync()
		{
			GetUsersResponse response = new GetUsersResponse();
			try
			{
				var data = await _repository.GetAllAsync();
				response.Data = _mapper.Map<List<DTO.UserDTO>>(data);
				response.Success= true;
				response.Message = "Succsefully got all users";
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

		public async Task<UserDTO> GetByIdAsync(int id)
		{
			return _mapper.Map<UserDTO>(await _repository.GetByIdAsync(id));
		}

		public async Task<AddUserResponse> AddAsync(RegisterDTO user)
		{
			AddUserResponse response = new AddUserResponse();
			try
			{
				var requestUser = _mapper.Map<BLL.Models.User>(user);

				if (user.Password != null && !string.IsNullOrWhiteSpace(user.Password))
				{
					if (!IsPasswordValid(user.Password) || !IsEmailValid(user.Email))
					{
						response.Success = false;

						if (!IsPasswordValid(user.Password) && !IsEmailValid(user.Email))
						{
							response.Message = "Password and Email aren't valid.";
						}
						else if (!IsEmailValid(user.Email))
						{
							response.Message = "Email isn't valid.";
						}
						else
						{
							response.Message = "Password isn't valid.";
						}

						return response;
					}
					CreatePasswordHash(user.Password, out byte[] passwordHash, out byte[] passwordSalt);
					requestUser.PasswordHash = passwordHash;
					requestUser.PasswordSalt = passwordSalt;
				}
				var addedUser = await _repository.AddAsync(_mapper.Map<DAL.Models.User>(requestUser));

				if (addedUser != null)
				{
					response.Data = _mapper.Map<DTO.UserDTO>(addedUser);
					response.Success = true;
					response.Message = $"Succsesfully added {addedUser.Username}";
					return response;

				}
				response.Data = null;
				response.Success = false;
				response.Message = $"Could not find {requestUser.Username}";
				return response;


			}
			catch (Exception ex)
			{
				response.Data = null;
				response.Success = false;
				response.Error = ex.Message;
				response.Message = "";

				return response;
			}
		}
		public async Task<EditUserResponse> UpdateAsync(RegisterDTO user)
		{
			EditUserResponse response = new EditUserResponse();
			try
			{
				var requestUser = _mapper.Map<BLL.Models.User>(user);

				if (user.Password != null && !string.IsNullOrWhiteSpace(user.Password) )
				{
					if (!IsPasswordValid(user.Password) || !IsEmailValid(user.Email))
					{
						response.Success = false;

						if (!IsPasswordValid(user.Password) && !IsEmailValid(user.Email))
						{
							response.Message = "Password and Email aren't valid.";
						}
						else if (!IsEmailValid(user.Email))
						{
							response.Message = "Email isn't valid.";
						}
						else
						{
							response.Message = "Password isn't valid.";
						}

						return response;
					}
					CreatePasswordHash(user.Password, out byte[] passwordHash, out byte[] passwordSalt);
					requestUser.PasswordHash = passwordHash;
					requestUser.PasswordSalt = passwordSalt;
				}
				var modifiedUser = await _repository.UpdateAsync(_mapper.Map<DAL.Models.User>(requestUser));

				if(modifiedUser != null) {
					response.Data = _mapper.Map<DTO.UserDTO>(modifiedUser);
					response.Success = true;
					response.Message = $"Succsesfully modified {modifiedUser.Username}";
					return response;

				}
				response.Data = null;
				response.Success = false;
				response.Message = $"Could not find {requestUser.Username}";
				return response;


			}
			catch (Exception ex)
			{
				response.Data = null;
				response.Success = false;
				response.Error =ex.Message;
				response.Message = "";

				return response;
			}
			
		}

		public async Task<DeleteUserResponse> DeleteAsync(int id)
		{
			DeleteUserResponse response = new();
			try
			{
				var deletedUser = await _repository.DeleteAsync(id);
				if(deletedUser != null)
				{
					response.Data = _mapper.Map<UserDTO>(deletedUser);
					response.Success = true;
					response.Message = $"User {deletedUser.Username} was succsefully deleted";
					return response;
				}
				response.Success = false;
				response.Message = $"User was not found";
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




		#endregion
		#region Private Methods

		static bool IsPasswordValid(string password)
		{
			// Define the regular expression pattern
			string pattern = @"^(?=.*[A-Z])(?=.*\d).{6,}$";

			// Use Regex.IsMatch to check if the password matches the pattern
			bool isMatch = Regex.IsMatch(password, pattern);

			return isMatch;
		}

		static bool IsEmailValid(string email)
		{
			// Define the regular expression pattern
			string pattern = @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$";

			// Use Regex.IsMatch to check if the password matches the pattern
			bool isMatch = Regex.IsMatch(email, pattern);

			return isMatch;
		}

		private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
		{
			using (var hmac = new HMACSHA512())
			{
				passwordSalt = hmac.Key;
				passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
			}
		}

		private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
		{
			using (var hmac = new HMACSHA512(passwordSalt))
			{
				var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
				return computedHash.SequenceEqual(passwordHash);
			}
		}

		#endregion
	}
}
