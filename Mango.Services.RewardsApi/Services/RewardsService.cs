using Mango.Services.RewardsApi.Data;
using Mango.Services.RewardsApi.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mango.Services.EmailApi.Services
{
    public class RewardsService : IRewardsService
    {
        private DbContextOptions<AppDbContext> _dbOptions;

        public RewardsService(DbContextOptions<AppDbContext> dbOptions)
        {
            _dbOptions = dbOptions;
        }

        public async Task UpdateRewards(RewardsMesaage rewardsMesaage)
        {
            try
            {
                Rewards rewards = new()
                {
                    OrderId= rewardsMesaage.OrderId,
                    RewardActivity= rewardsMesaage.RewardsActivity,
                    Userid = rewardsMesaage.UserId,
                    RewardDate = DateTime.Now
                };
                await using var _db = new AppDbContext(_dbOptions);
                await _db.Reward.AddAsync(rewards);
                await _db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                
            }
        }
    }
}
