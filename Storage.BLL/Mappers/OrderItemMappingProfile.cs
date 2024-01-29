using AutoMapper;
using Storage.BLL.DTO.OrderItem;
using Storage.DAL.Entities;

namespace Storage.BLL.Mappers
{
	public class OrderItemMappingProfile : Profile
	{
		public OrderItemMappingProfile()
		{
			CreateMap<OrderItemDto, OrderItem>();
			CreateMap<OrderItem, OrderItemWithProductDto>();
		}
	}
}
