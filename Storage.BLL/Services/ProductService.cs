using AutoMapper;
using Storage.BLL.DTO.Product;
using Storage.BLL.Services.Interfaces;
using Storage.DAL.Entities;
using Storage.DAL.Repositories.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Storage.BLL.Services
{
	public class ProductService : BaseService, IProductService
	{
		public ProductService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper) { }

		public async Task<IEnumerable<ProductDto>> GetProducts()
		{
			var products = await _unitOfWork.ProductRepository.GetAll();

			return _mapper.Map<IEnumerable<ProductDto>>(products);
		}
		public async Task<ProductDto> GetProduct(long id)
		{
			var product = await _unitOfWork.ProductRepository.GetFirstOrDefault(p => p.Id == id);

			if (product == null)
			{
				throw new KeyNotFoundException("Product with this id does not exists");
			}

			return _mapper.Map<ProductDto>(product);
		}

		public async Task<ProductDto> Create(CreateProductDto productDto)
		{
			var productEntity = _mapper.Map<Product>(productDto);


			if ((await _unitOfWork.ProductRepository.GetAll(p => p.Name == productDto.Name)).Any())
			{
				throw new ValidationException("Product with the same name already exists");
			}

			await _unitOfWork.ProductRepository.Add(productEntity);
			await _unitOfWork.Save();

			return _mapper.Map<ProductDto>(productEntity);
		}

		public async Task<ProductDto> Edit(long id, EditProductDto productDto)
		{
			var product = await _unitOfWork.ProductRepository.GetFirstOrDefault(p => p.Id == id);

			if (product == null)
			{
				throw new KeyNotFoundException("Product with this id does not exists");
			}

			product.Name = productDto.Name;
			product.QuantityInStock = productDto.QuantityInStock;
			product.Price = productDto.Price;

			_unitOfWork.ProductRepository.Update(product);
			await _unitOfWork.Save();

			return _mapper.Map<ProductDto>(product);
		}

		public async Task<ProductDto> Delete(long id)
		{
			var product = await _unitOfWork.ProductRepository.GetFirstOrDefault(p => p.Id == id);

			if (product == null)
			{
				throw new KeyNotFoundException("Product with this id does not exists");
			}

			_unitOfWork.ProductRepository.Remove(product);
			await _unitOfWork.Save();

			return _mapper.Map<ProductDto>(product);
		}

		public async Task<bool> RenewProductQuantity()
		{
			try
			{
				var purchaseQueues = await _unitOfWork.PurchaseQueueRepository.GetAll();

				foreach (var purchaseQueue in purchaseQueues)
				{
					var product = await _unitOfWork.ProductRepository.GetFirstOrDefault(p => p.Id == purchaseQueue.ProductId);
					product.QuantityInStock += purchaseQueue.QuantityToPurchase + 10;
					_unitOfWork.ProductRepository.Update(product);

					_unitOfWork.PurchaseQueueRepository.Remove(purchaseQueue);
				}

				var orders = await _unitOfWork.OrderRepository.GetAll(o => o.OrderStatus == Shared.Enums.OrderStatus.WaitingForProducts);
				foreach (var order in orders)
				{
					order.OrderStatus = Shared.Enums.OrderStatus.Pending;
					_unitOfWork.OrderRepository.Update(order);
				}

				await _unitOfWork.Save();

				return true;
			}
			catch (Exception)
			{
				return false;
			}
		}
	}
}
