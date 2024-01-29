using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Storage.DAL.Entities
{
	public class PurchaseQueue
	{
		[Key]
		public long Id { get; set; }
		public required long ProductId { get; set; }
		[ForeignKey(nameof(ProductId))]
		public Product Product { get; set; }
		public int QuantityToPurchase { get; set; }
		public DateTime QueueDate { get; set; }
	}
}
