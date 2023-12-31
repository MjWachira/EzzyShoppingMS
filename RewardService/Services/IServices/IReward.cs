using RewardsService.Models;

namespace RewardsService.Services.IServices
{
    public interface IReward
    {

        Task AddReward(Reward reward);
    }
}
