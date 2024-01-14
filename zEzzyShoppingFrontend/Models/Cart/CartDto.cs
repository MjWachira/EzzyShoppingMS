namespace zEzzyShoppingFrontend.Models.Cart
{
    public class CartDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid ProductId { get; set; }
        public double ProductPrice { get; set; }
        public int ProductCount { get; set; }
        public string CouponCode { get; set; } = string.Empty;
        public double Discount { get; set; }
        public int ProductTotal { get; set; }
    }
}
