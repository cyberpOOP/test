using Storage.BLL.DTO.Order;
using Storage.BLL.DTO.User;

namespace Storage.BLL.Services.Interfaces
{
	public interface IOrderService
	{
		Task<IEnumerable<UserOrderDto>> GetOrders();
		Task<IEnumerable<UserOrderDto>> GetNotDoneOrders();
		Task<UserOrderDto> GetOrder(long id);
		Task<IEnumerable<UserOrderDto>> GetUserOrders(UserEmailDto userEmailDto);
		Task<OrderDto> CreateOrder(CreateOrderDto orderDto);
		Task<OrderDto> MarkOrderAsDone(long orderId);
	}
}
