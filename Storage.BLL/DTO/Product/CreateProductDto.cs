namespace Storage.BLL.DTO.Product
{
	public class CreateProductDto
	{
		public string Name { get; set; }
		public int QuantityInStock { get; set; }
		public decimal Price { get; set; }
	}
}
