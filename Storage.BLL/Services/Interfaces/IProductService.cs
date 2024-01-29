using Storage.BLL.DTO.Product;

namespace Storage.BLL.Services.Interfaces
{
	public interface IProductService
	{
		Task<IEnumerable<ProductDto>> GetProducts();
		Task<ProductDto> GetProduct(long id);
		Task<ProductDto> Create(CreateProductDto productDto);
		Task<ProductDto> Edit(long id, EditProductDto productDto);
		Task<ProductDto> Delete(long id);
		Task<bool> RenewProductQuantity();
	}
}
