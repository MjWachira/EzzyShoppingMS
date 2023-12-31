using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RewardsService.Data;
using RewardsService.Models;
using RewardsService.Services.IServices;

namespace RewardsService.Services
{
    public class RewardsServices:IReward
    {
        private DbContextOptions<ApplicationDbContext> options;

        public RewardsServices(DbContextOptions<ApplicationDbContext> options)
        {
            this.options = options;
        }

        public async Task AddReward(Reward reward)
        {
            var _db = new ApplicationDbContext(options);
            _db.Rewards.Add(reward);
            await _db.SaveChangesAsync();
        }
    }
}
