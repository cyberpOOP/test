using System.ComponentModel.DataAnnotations;

namespace Storage.DAL.Entities
{
	public class Product
	{
		[Key]
		public long Id { get; set; }
		public string Name { get; set; }
		public int QuantityInStock { get; set; }
		public decimal Price { get; set; }
	}
}
