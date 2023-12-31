namespace OrderService.Models
{
    public class Orders
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid CartItemsId { get; set; }
        //public Guid ProductId { get; set; }
        public double ProductTotal { get; set; }
        public string CouponCode { get; set; } = string.Empty;
        public double Discount { get; set; }
        public DateTime OrderDate { get; set; }= DateTime.Now;
        public string? StripeSessionId { get; set; }
        public string Status { get; set; } = "Pending";
        public string PaymentIntent { get; set; } = string.Empty;
    }
}
