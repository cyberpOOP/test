using Storage.DAL.Entities;

namespace Storage.DAL.Repositories.Interfaces
{
	public interface IUnitOfWork
	{
		IRepository<Order> OrderRepository { get; }
		IRepository<OrderItem> OrderItemRepository { get; }
		IRepository<Product> ProductRepository { get; }
		IRepository<PurchaseQueue> PurchaseQueueRepository { get; }
		IRepository<User> UserRepository { get; }
		Task Save();
	}
}
