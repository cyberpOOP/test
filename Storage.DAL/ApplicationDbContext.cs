using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Storage.DAL.Entities;

namespace Storage.DAL
{
	public class ApplicationDbContext : DbContext
	{
		public ApplicationDbContext() { }
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
		public DbSet<Order> Orders { get; set; }
		public DbSet<OrderItem> OrderItems { get; set; }
		public DbSet<Product> Products { get; set; }
		public DbSet<PurchaseQueue> PurchaseQueues { get; set; }
		public DbSet<User> Users { get; set; }

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);

			builder.Entity<Order>()
				.HasOne(o => o.User)
				.WithMany(u => u.Orders)
				.HasForeignKey(o => o.UserId)
				.OnDelete(DeleteBehavior.Restrict);

			builder.Entity<OrderItem>()
				.HasKey(oi => new { oi.OrderId, oi.ProductId });

			builder.Entity<OrderItem>()
				.HasOne(oi => oi.Order)
				.WithMany(o => o.OrderItems);

			builder.Entity<Product>()
				.HasIndex(p => p.Name)
				.IsUnique();
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			//var configuration = new ConfigurationBuilder()
			//		.SetBasePath(Directory.GetCurrentDirectory())
			//		.AddJsonFile("appsettings.json")
			//		.Build();

			//optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
		}
	}
}
