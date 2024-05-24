using Mango.Services.RewardsApi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mango.Services.EmailApi.Services
{
    public interface IRewardsService
    {
        Task UpdateRewards(RewardsMesaage rewardsMesaage);
    }
}
