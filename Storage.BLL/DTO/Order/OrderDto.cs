using Storage.BLL.DTO.OrderItem;

namespace Storage.BLL.DTO.Order
{
	public class OrderDto
	{
		public long Id { get; set; }
		public List<OrderItemDto> OrderItems { get; set; }
	}
}
