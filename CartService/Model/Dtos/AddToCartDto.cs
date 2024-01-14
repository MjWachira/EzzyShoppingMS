namespace CartService.Model.Dtos
{
    public class AddToCartDto
    {
        public Guid UserId { get; set; }
        public Guid ProductId { get; set; }
        public double ProductPrice { get; set; }
        public int ProductCount { get; set; } = 1;
        public string CouponCode { get; set; } = string.Empty;
        public double Discount { get; set; }
        public int ProductTotal { get; set; }
    }
}
