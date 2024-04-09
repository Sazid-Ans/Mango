using MangoWeb.Models;
using MangoWeb.Service.Iservice;
using MangoWeb.Utility;
using static MangoWeb.Utility.SD;

namespace MangoWeb.BaseService
{
    public class CouponService : ICouponService
    {
        private readonly IBaseService _bs;
        public string URI;
        public CouponService(IBaseService baseSerice)
        {
            _bs = baseSerice;
            URI = SD.CouponAPIbase + "/Api/Coupon/";
        }
        public Task<ResponseDto> createCoupon(CouponDto coupon)
        {
            var uri = URI + "CreateCoupon";
            RequestDto dto = new RequestDto { ApiType = ApiType.Post, URI = uri, Data = coupon };

            var response = _bs.SendAsync(dto);
            return response;
        }

        public Task<ResponseDto> DeleteCoupon(int couponId)
        {
            var uri = URI + "DeleteCoupun?id=" + couponId; 
            RequestDto dto = new RequestDto { ApiType = ApiType.Delete, URI = uri };
            var response = _bs.SendAsync(dto);
            return response;
        }

        public Task<ResponseDto> GetAllCoupon()
        {
            var uri = URI + "getAllCoupon";
            RequestDto dto = new RequestDto { ApiType = ApiType.Get , URI = uri };
            var response = _bs.SendAsync(dto);
            return response;
        }

        public Task<ResponseDto> GetCoupon(string couponCode)
        {
            var uri = URI + "getCouponByCode/" + couponCode;
            RequestDto dto = new RequestDto { ApiType = ApiType.Get, URI = uri };
            var response = _bs.SendAsync(dto);
            return response;
        }

        public Task<ResponseDto> GetCouponById(int couponid)
        {
            var uri = URI + "getCoupon/" + couponid;
            RequestDto dto = new RequestDto { ApiType = ApiType.Get, URI = uri };
            var response = _bs.SendAsync(dto);
            return response;
        }

        public Task<ResponseDto> updateCoupon(CouponDto coupon)
        {
            var uri = URI + "getCouponByCode/" ;
            RequestDto dto = new RequestDto { ApiType = ApiType.Put, URI = uri , Data = coupon };
            var response = _bs.SendAsync(dto);
            return response;
        }
    }
}
