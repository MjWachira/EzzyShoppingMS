namespace RewardsService.Models
{
    public class Reward
    {
        public Guid Id { get; set; }
        public Guid OrderId { get; set; }
        public double ProductTotal { get; set; }
        public double Points { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

    }
}
