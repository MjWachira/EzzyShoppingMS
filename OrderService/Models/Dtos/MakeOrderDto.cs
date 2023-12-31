namespace OrderService.Models.Dtos
{
    public class MakeOrderDto
    {
        public Guid UserId { get; set; }
        public Guid CartItemsId { get; set; }
        public double ProductTotal { get; set; }
        public string CouponCode { get; set; } = string.Empty;
        public double Discount { get; set; }
    }
}
