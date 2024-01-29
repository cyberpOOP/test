using AutoMapper;
using Storage.BLL.DTO.User;
using Storage.DAL.Entities;

namespace Storage.BLL.Mappers
{
	public class UserMappingProfile : Profile
	{
		public UserMappingProfile()
		{
			CreateMap<UserLoginDto, User>();
			CreateMap<UserRegisterDto, User>()
				.ForMember(dest => dest.Password, opt => opt.Ignore());
			CreateMap<User, UserDto>();
		}
	}
}
