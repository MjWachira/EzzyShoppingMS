namespace CartService.Model.Dtos
{
    public class AddToCartDto
    {
        public Guid UserId { get; set; }
        public Guid ProductId { get; set; }
        public int ProductPrice { get; set; }
        public int ProductCount { get; set; } = 1;
    }
}
