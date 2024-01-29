using AutoMapper;
using Storage.BLL.DTO.User;
using Storage.BLL.Services.Interfaces;
using Storage.DAL;
using Storage.DAL.Entities;
using Storage.DAL.Repositories.Interfaces;
using System.Security.Cryptography;
using System.Text;

namespace Storage.BLL.Services
{
	public class AuthService : BaseService, IAuthService
	{
		public AuthService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper) { }

		public async Task<UserDto> Login(UserLoginDto userDto)
		{
			var user = await _unitOfWork.UserRepository.GetFirstOrDefault(u => u.Email == userDto.Email);

			if (user == null || !VerifyPassword(userDto.Password, user.Password))
			{
				throw new Exception("Invalid email or password.");
			}

			return _mapper.Map<UserDto>(user);
		}

		public async Task<UserDto> Register(UserRegisterDto userDto)
		{
			if ((await _unitOfWork.UserRepository.GetAll(u => u.Email == userDto.Email)).Any())
			{
				throw new Exception("User with this email already exists.");
			}

			var hashedPassword = HashPassword(userDto.Password);

			var newUser = _mapper.Map<User>(userDto);
			newUser.Password = hashedPassword;

			await _unitOfWork.UserRepository.Add(newUser);
			await _unitOfWork.Save();

			return _mapper.Map<UserDto>(newUser);
		}

		private string HashPassword(string password)
		{
			var sha = SHA256.Create();
			var asByteArray = Encoding.Default.GetBytes(password);

			var hashedPassword = sha.ComputeHash(asByteArray);
			return Convert.ToBase64String(hashedPassword);
		}


		private bool VerifyPassword(string providedPassword, string storedPassword)
		{
			byte[] storedHashedPassword = Convert.FromBase64String(storedPassword);

			using (var sha = SHA256.Create())
			{
				byte[] providedHashedPassword = sha.ComputeHash(Encoding.Default.GetBytes(providedPassword));

				return storedHashedPassword.SequenceEqual(providedHashedPassword);
			}
		}
	}
}
