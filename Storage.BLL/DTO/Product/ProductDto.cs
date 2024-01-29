namespace Storage.BLL.DTO.Product
{
	public class ProductDto
	{
		public long Id { get; set; }
		public string Name { get; set; }
		public int QuantityInStock { get; set; }
		public decimal Price { get; set; }

        public override string ToString()
        {
			return $"Id: {Id}\nName: {Name}\nQuantity in stock: {QuantityInStock}\nPrice: {Price}";
        }
    }
}
