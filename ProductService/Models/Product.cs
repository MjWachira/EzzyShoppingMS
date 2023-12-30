namespace ProductService.Models
{
    public class Product
    {
        public Guid Id { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string ProductDescription { get; set; } = string.Empty;
        public int Price { get; set; }
        public List<ProductImage> ProductImages { get; set; } = new List<ProductImage>();
    }   
}
