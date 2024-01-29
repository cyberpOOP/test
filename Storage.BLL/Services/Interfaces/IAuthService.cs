using Storage.BLL.DTO.User;

namespace Storage.BLL.Services.Interfaces
{
	public interface IAuthService
	{
		Task<UserDto> Login(UserLoginDto userDto);
		Task<UserDto> Register(UserRegisterDto userDto);
	}
}
