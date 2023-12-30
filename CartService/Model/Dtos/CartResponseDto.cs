namespace CartService.Model.Dtos
{
    public class CartResponseDto
    {
        public Guid ProductId { get; set; }
        public int ProductPrice { get; set; }
        public int ProductCount { get; set; }
        public int ProductTotal { get; set; }
    }
}
