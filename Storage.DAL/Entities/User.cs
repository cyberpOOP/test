using System.ComponentModel.DataAnnotations;

namespace Storage.DAL.Entities
{
	public class User
	{
		[Key]
		public long Id { get; set; }
		public string Name { get; set; }
		[EmailAddress]
		public string Email { get; set; }
		public string Password { get; set; }
		public List<Order> Orders { get; set; } = new List<Order>();
	}
}
