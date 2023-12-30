namespace CartService.Model
{
    public class Cart
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid ProductId { get; set; }
        public int ProductPrice { get; set; }
        public int ProductCount { get; set; }
        public int ProductTotal { get; set; }
    }
}
