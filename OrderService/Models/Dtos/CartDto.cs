namespace OrderService.Models.Dtos
{
    public class CartDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid ProductId { get; set; }
        public int ProductPrice { get; set; }
        public int ProductCount { get; set; }
        public int ProductTotal { get; set; }
    }
}
