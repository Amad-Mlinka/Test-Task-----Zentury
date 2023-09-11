using AutoMapper;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;
using System.Security.Cryptography;
using TestTask.BLL.Interface;
using TestTask.DAL.Interface;
using TestTask.BLL.Models;
using TestTask.BLL.Response;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using TestTask.BLL.DTO;
using System.Text.RegularExpressions;

namespace TestTask.BLL
{
	public class AuthBLL : IAuthBLL
	{
		private readonly IUserRepository _repository; 
		private readonly ILogRepository _logRepository; 

		private readonly IMapper _mapper;
		private readonly IConfiguration _configuration;

		public AuthBLL(IUserRepository repository, ILogRepository logRepository, IMapper mapper, IConfiguration configuration) 
		{
			_repository = repository;
			_logRepository = logRepository;
			_mapper = mapper;
			_configuration = configuration;
		}

		public async Task<AuthResponse> RegisterUser(RegisterDTO request)
		{
			BLL.Models.User user = new BLL.Models.User();
			AuthResponse response = new();

			try
			{
				user.Username = request.Username;
				user.Email = request.Email;
				if(await UserExists(user))
				{
					response.Success = false;
					response.Message = "User already exists";
					return response;

				}
				if (!IsPasswordValid(request.Password) || !IsEmailValid(request.Email))
				{
					response.Success = false;

					if (!IsPasswordValid(request.Password) && !IsEmailValid(request.Email))
					{
						response.Message = "Password and Email aren't valid.";
					}
					else if (!IsEmailValid(request.Email))
					{
						response.Message = "Email isn't valid.";
					}
					else
					{
						response.Message = "Password isn't valid.";
					}

					return response;
				}

				CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);
				user.PasswordHash = passwordHash;
				user.PasswordSalt = passwordSalt;

				string token = CreateToken(user);
				var refreshToken = GenerateRefreshToken();
				user.RefreshToken = refreshToken.Token;
				user.TokenCreated = refreshToken.Created;
				user.TokenExpires = refreshToken.Expires;

				DAL.Models.User registeredUser = _mapper.Map<DAL.Models.User>(user);

				await _repository.AddAsync(registeredUser);

				response.Success = true;
				response.Data = user;

				return response;


			}catch (Exception ex)
			{
				response.Success= false;
				response.Error = ex.Message;
				return response;
			}
		}

		public async Task<LoginResponse> LoginUser(LoginDTO request)
		{
			BLL.Models.User user = new BLL.Models.User();
			LoginResponse response = new();

			try
			{
				user.Username = request.Username;
				var foundUser = await _repository.GetByUsernameAsync(user.Username);
				if (foundUser == null)
				{
					response.Success = false;
					response.Message = "That username doesn't exist";
					return response;

				}
				bool verified = VerifyPasswordHash(request.Password, foundUser.PasswordHash, foundUser.PasswordSalt);
				if (verified)
				{	
					string token = CreateToken(user,request.RememberMe);
					var refreshToken = GenerateRefreshToken();
					user.RefreshToken = refreshToken.Token;
					user.TokenCreated = refreshToken.Created;
					user.TokenExpires = refreshToken.Expires;
					user.Email = foundUser.Email;

					await _repository.UpdateTokenAsync(_mapper.Map<DAL.Models.User>(user));
					response.Success = true;
					response.Data = user;
					response.Token = token;
					response.RefreshToken = refreshToken;
					await _logRepository.LogLoginAttempt(foundUser, true);
					return response;


				}
				else
				{
					response.Success = false;
					response.Data = null;
					response.Token = null;
					response.RefreshToken = null;
					response.Message = "Wrong password";
					await _logRepository.LogLoginAttempt(foundUser, false);
				}

				return response;


			}
			catch (Exception ex)
			{
				response.Success = false;
				response.Error = ex.Message;
				return response;
			}

		}


		#region Private

		private async Task<bool> UserExists(User user)
		{
			if((await _repository.GetByUsernameAsync(user.Username) != null) || (await _repository.GetByEmailAsync(user.Email) != null)){
				return true;
			};
			return false;
		}

		static bool IsPasswordValid(string password)
		{
			string pattern = @"^(?=.*[A-Z])(?=.*\d).{6,}$";

			bool isMatch = Regex.IsMatch(password, pattern);

			return isMatch;
		}

		static bool IsEmailValid(string email)
		{
			string pattern = @"^[\w\.-]+@[\w\.-]+\.\w+$";

			bool isMatch = Regex.IsMatch(email, pattern);

			return isMatch;
		}

		private string CreateToken(User user, bool rememberMe = false)
		{
			List<Claim> claims = new List<Claim>
			{
				new Claim(ClaimTypes.Name, user.Username),
				new Claim(ClaimTypes.Role, "Admin")
			};

			var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
				_configuration.GetSection("AppSettings:Token").Value));

			var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

			var token = new JwtSecurityToken(
				claims: claims,
				expires: rememberMe ? DateTime.UtcNow.AddDays(31) : DateTime.UtcNow.AddDays(1),
				signingCredentials: creds);

			var jwt = new JwtSecurityTokenHandler().WriteToken(token);

			return jwt;
		}


		private RefreshToken GenerateRefreshToken()
		{
			var refreshToken = new RefreshToken
			{
				Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
				Expires = DateTime.UtcNow.AddDays(7),
				Created = DateTime.UtcNow
			};

			return refreshToken;
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
