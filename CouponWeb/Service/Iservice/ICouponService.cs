using MangoWeb.Models;

namespace MangoWeb.Service.Iservice
{
    public interface ICouponService
    {
        Task<ResponseDto> GetCoupon(string couponCode);
        Task<ResponseDto> GetCouponById(int couponid);
        Task<ResponseDto> GetAllCoupon();
        Task<ResponseDto> updateCoupon(CouponDto coupon);
        Task<ResponseDto> createCoupon(CouponDto coupon);
        Task<ResponseDto> DeleteCoupon(int couponId);

    }
}
