using AutoMapper;
using Storage.BLL.DTO.Order;
using Storage.DAL.Entities;

namespace Storage.BLL.Mappers
{
	public class OrderMappingProfile : Profile
	{
		public OrderMappingProfile()
		{
			CreateMap<CreateOrderDto, Order>()
			   .ForMember(dest => dest.OrderItems, act => act.Ignore());
			CreateMap<Order, OrderDto>()
			   .ForMember(dest => dest.OrderItems, act => act.Ignore());
			CreateMap<Order, UserOrderDto>();
		}
	}
}
