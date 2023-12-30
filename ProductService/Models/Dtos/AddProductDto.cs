namespace ProductService.Models.Dtos
{
    public class AddProductDto
    {
        public string ProductName { get; set; } = string.Empty;
        public string ProductDescription { get; set; } = string.Empty;
        public int Price { get; set; }
    }
}
