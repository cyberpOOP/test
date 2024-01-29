using Storage.BLL.DTO.Product;

namespace Storage.BLL.DTO.OrderItem
{
	public class OrderItemWithProductDto
	{
		public ProductDto Product { get; set; }
		public int Quantity { get; set; }
	}
}
