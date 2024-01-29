using Storage.BLL.DTO.OrderItem;

namespace Storage.BLL.DTO.Order
{
	public class CreateOrderDto
	{
		public string UserEmail { get; set; } = string.Empty;
		public List<OrderItemDto> OrderItems { get; set; } = new();
	}
}
