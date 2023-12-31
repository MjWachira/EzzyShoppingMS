namespace MailService.Models.Dtos
{
    public class RewardDto
    {
        public Guid OrderId { get; set; }
        public double ProductTotal { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }
}
