using Storage.Shared.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Storage.DAL.Entities
{
	public class Order
	{
		[Key]
		public long Id { get; set; }
		public required long UserId { get; set; }
		[ForeignKey(nameof(UserId))]
		public User User { get; set; }
		public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
		public DateTime OrderDate { get; set; }
		public OrderStatus OrderStatus { get; set; }
	}
}
