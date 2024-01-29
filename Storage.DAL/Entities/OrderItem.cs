using System.ComponentModel.DataAnnotations.Schema;

namespace Storage.DAL.Entities
{
	public class OrderItem
	{
		public long OrderId { get; set; }
		[ForeignKey(nameof(OrderId))]
		public Order Order { get; set; }
		public long ProductId { get; set; }
		[ForeignKey(nameof(ProductId))]
		public Product Product { get; set; }
		public int Quantity { get; set; }
	}
}
