namespace Mango.Services.RewardsApi.Model
{
    public class Rewards
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public string Userid { get; set; }
        public DateTime RewardDate { get; set; }
        public int RewardActivity { get; set; }
    }
}
