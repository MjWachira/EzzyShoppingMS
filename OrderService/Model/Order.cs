namespace OrderService.Model
{
    public class Order
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid CartItemsId { get; set; }
       // public Guid ProductId { get; set; }
        public double ProductTotal { get; set; }
        public string CouponCode { get; set; } = string.Empty;
        public double Discount { get; set; }
        public DateTime OrderDate { get; set; }= DateTime.Now;
    }
}
