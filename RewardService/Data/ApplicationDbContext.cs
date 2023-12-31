using Microsoft.EntityFrameworkCore;
using RewardsService.Models;

namespace RewardsService.Data
{
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Reward> Rewards { get; set; }
    }
}