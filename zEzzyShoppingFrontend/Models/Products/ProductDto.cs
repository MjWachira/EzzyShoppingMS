namespace zEzzyShoppingFrontend.Models.Products
{
    public class ProductDto
    {
        public string Id { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public decimal Price { get; set; }
        public List<ProductImage> ProductImages { get; set; }
    }
    public class ProductImage
    {
        public string Image { get; set; }
    }
}
