using Storage.DAL.Entities;
using Storage.DAL.Repositories.Interfaces;
namespace Storage.DAL.Repositories
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly ApplicationDbContext _context;

		private IRepository<Order> orderRepository;
		private IRepository<OrderItem> orderItemRepository;
		private IRepository<Product> productRepository;
		private IRepository<PurchaseQueue> purchaseQueueRepository;
		private IRepository<User> userRepository;
		public UnitOfWork(ApplicationDbContext context)
		{
			_context = context;
		}
		public IRepository<Order> OrderRepository
		{
			get
			{
				if (orderRepository == null)
				{
					orderRepository = new Repository<Order>(_context);
				}
				return orderRepository;
			}
		}
		public IRepository<OrderItem> OrderItemRepository
		{
			get
			{
				if (orderItemRepository == null)
				{
					orderItemRepository = new Repository<OrderItem>(_context);
				}
				return orderItemRepository;
			}
		}
		public IRepository<Product> ProductRepository
		{
			get
			{
				if (productRepository == null)
				{
					productRepository = new Repository<Product>(_context);
				}
				return productRepository;
			}
		}
		public IRepository<PurchaseQueue> PurchaseQueueRepository
		{
			get
			{
				if (purchaseQueueRepository == null)
				{
					purchaseQueueRepository = new Repository<PurchaseQueue>(_context);
				}
				return purchaseQueueRepository;
			}
		}
		
		public IRepository<User> UserRepository
		{
			get
			{
				if (userRepository == null)
				{
					userRepository = new Repository<User>(_context);
				}
				return userRepository;
			}
		}

		public async Task Save()
		{
			await _context.SaveChangesAsync();
		}
	}
}
