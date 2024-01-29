using AutoMapper;
using Storage.BLL.DTO.Product;
using Storage.DAL.Entities;

namespace Storage.BLL.Mappers
{
	public class ProductMappingProfile : Profile
	{
		public ProductMappingProfile()
		{
			CreateMap<CreateProductDto, Product>();
			CreateMap<Product, ProductDto>();
		}
	}
}
