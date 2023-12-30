namespace ProductService.Models.Dtos
{
    public class ProductAndImagesResponseDto
    {
        public Guid Id { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string ProductDescription { get; set; } = string.Empty;
        public int Price { get; set; }
        public List<AddProductImageDto> ProductImages { get; set; } = new List<AddProductImageDto>();
    }
}
