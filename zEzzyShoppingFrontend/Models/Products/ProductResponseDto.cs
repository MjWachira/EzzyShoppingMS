namespace zEzzyShoppingFrontend.Models.Products
{
    public class ProductResponseDto
    {
        public string ErrorMessage { get; set; }
        public List<ProductDto> Result { get; set; }
        public bool IsSuccess { get; set; }
    }
}
