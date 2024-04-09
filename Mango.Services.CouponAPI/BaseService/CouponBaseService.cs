using Mango.Services.CouponAPI.Data;
using Mango.Services.CouponAPI.Models;

namespace Mango.Services.CouponAPI.BaseService
{
    public class CouponBaseService
    {
        private readonly AppDbContext _db;
        public CouponBaseService(AppDbContext db)
        {
          _db = db;
        }

        public async Task<Coupon> Get()
        {
            return await _db.FindAsync<Coupon>();
        }
    }
}
