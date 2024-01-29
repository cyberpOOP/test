using Storage.BLL.DTO.OrderItem;
using Storage.BLL.DTO.User;
using Storage.Shared.Enums;

namespace Storage.BLL.DTO.Order
{
	public class UserOrderDto
	{
		public long Id { get; set; }
		public List<OrderItemWithProductDto> OrderItems { get; set; }
		public UserDto User { get; set; }
		public OrderStatus OrderStatus { get; set; }
	}
}
