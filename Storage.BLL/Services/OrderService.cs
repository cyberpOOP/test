using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Storage.BLL.DTO.Order;
using Storage.BLL.DTO.OrderItem;
using Storage.BLL.DTO.User;
using Storage.BLL.Services.Interfaces;
using Storage.DAL.Entities;
using Storage.DAL.Repositories.Interfaces;
using Storage.Shared.Enums;

namespace Storage.BLL.Services
{
	public class OrderService : BaseService, IOrderService
	{
		public OrderService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper) { }


		public async Task<IEnumerable<UserOrderDto>> GetOrders()
		{
			var orders = await _unitOfWork.OrderRepository.GetAll(includeProperties: "OrderItems.Product,User");

			return _mapper.Map<IEnumerable<UserOrderDto>>(orders);
		}
		public async Task<IEnumerable<UserOrderDto>> GetNotDoneOrders()
		{
			var orders = await _unitOfWork.OrderRepository.GetAll(o => o.OrderStatus != OrderStatus.Done, includeProperties: "OrderItems.Product,User");

			return _mapper.Map<IEnumerable<UserOrderDto>>(orders);
		}
		public async Task<UserOrderDto> GetOrder(long id)
		{
			var order = await _unitOfWork.OrderRepository.GetFirstOrDefault(p => p.Id == id, "OrderItems.Product,User");

			if (order == null)
			{
				throw new KeyNotFoundException("Order with this id does not exists");
			}

			return _mapper.Map<UserOrderDto>(order);
		}

		public async Task<IEnumerable<UserOrderDto>> GetUserOrders(UserEmailDto userEmailDto)
		{
			var user = await _unitOfWork.UserRepository.GetFirstOrDefault(u => u.Email.Equals(userEmailDto.Email))
					   ?? throw new ValidationException("User with this email does not exist");

			var orders = await _unitOfWork.OrderRepository.GetAll(o => o.UserId == user.Id, "OrderItems.Product,User");

			return _mapper.Map<IEnumerable<UserOrderDto>>(orders);
		}

		public async Task<OrderDto> CreateOrder(CreateOrderDto orderDto)
		{
			var user = await _unitOfWork.UserRepository.GetFirstOrDefault(u => u.Email.Equals(orderDto.UserEmail))
					   ?? throw new ValidationException("User with this email does not exist");

			var orderEntity = _mapper.Map<Order>(orderDto);
			orderEntity.UserId = user.Id;

			bool purchaseQueueCreated = false;

			foreach (var orderItemDto in orderDto.OrderItems)
			{
				var product = await _unitOfWork.ProductRepository.GetFirstOrDefault(p => p.Id == orderItemDto.ProductId)
							  ?? throw new ValidationException($"Product with ID {orderItemDto.ProductId} was not found");

				CheckQuantityAndQueue(product, orderItemDto, orderEntity, ref purchaseQueueCreated);
			}

			orderEntity.OrderDate = DateTime.Now;

			orderEntity.OrderStatus = purchaseQueueCreated ? OrderStatus.WaitingForProducts : OrderStatus.Pending;

			orderEntity.OrderItems = null;

			await _unitOfWork.OrderRepository.Add(orderEntity);
			await _unitOfWork.Save();
			var orderId = orderEntity.Id;

			var orderItems = _mapper.Map<IEnumerable<OrderItem>>(orderDto.OrderItems);
			foreach (var orderItem in orderItems)
			{
				orderItem.OrderId = orderId;
			}
			await _unitOfWork.OrderItemRepository.AddRange(orderItems);
			await _unitOfWork.Save();

			var order = _mapper.Map<OrderDto>(orderEntity);
			order.OrderItems = orderDto.OrderItems;

			return order;
		}

		private void CheckQuantityAndQueue(Product product, OrderItemDto orderItemDto, Order orderEntity, ref bool purchaseQueueCreated)
		{

			if (product.QuantityInStock > 0 && product.QuantityInStock >= orderItemDto.Quantity)
			{
				var orderItemEntity = _mapper.Map<OrderItem>(orderItemDto);
				orderItemEntity.Product = product;
				orderEntity.OrderItems.Add(orderItemEntity);
			}
			else
			{
				var purchaseQueueEntity = new PurchaseQueue
				{
					ProductId = product.Id,
					QuantityToPurchase = orderItemDto.Quantity,
					QueueDate = DateTime.Now
				};

				_unitOfWork.PurchaseQueueRepository.Add(purchaseQueueEntity).Wait();
				purchaseQueueCreated = true;
			}
		}

		public async Task<OrderDto> MarkOrderAsDone(long orderId)
		{
			var order = await _unitOfWork.OrderRepository.GetFirstOrDefault(o => o.Id == orderId, "OrderItems");

			if (order == null)
			{
				throw new KeyNotFoundException($"Order with ID {orderId} not found.");
			}

			if(order.OrderStatus == OrderStatus.WaitingForProducts)
			{
				throw new InvalidOperationException("Cannot mark the order as done because some products are still waiting.");
			}

			order.OrderStatus = OrderStatus.Done;
			foreach (var orderItem in order.OrderItems) {
				var product = await _unitOfWork.ProductRepository.GetFirstOrDefault(p => p.Id == orderItem.ProductId);

				product.QuantityInStock -= orderItem.Quantity;

				_unitOfWork.ProductRepository.Update(product);
			}
			
			_unitOfWork.OrderRepository.Update(order);
			await _unitOfWork.Save();

			return _mapper.Map<OrderDto>(order);
		}
	}
}
