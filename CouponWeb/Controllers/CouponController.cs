using MangoWeb.Models;
using MangoWeb.Service.Iservice;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;

namespace MangoWeb.Controllers
{
    public class CouponController : Controller
    {
        private readonly ICouponService _couponService;
        public CouponController(ICouponService couponService)
        {
            _couponService = couponService;
        }
        public async Task<IActionResult> CouponIndex()
        {
            List<CouponDto> coupons = new();
            var response = await _couponService.GetAllCoupon();
            if (response != null && response.isSuccess)
            {
                coupons = JsonConvert.DeserializeObject<List<CouponDto>>(Convert.ToString(response.Result));
            }
            else
            {
                TempData["Error"] = response?.message;
            }
            return View(coupons);
        }
        public async Task<IActionResult> CouponCreate()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CouponCreate(CouponDto coupon)
        {
            if (ModelState.IsValid)
            {
                var response =await _couponService.createCoupon(coupon);
                if (response != null && response.isSuccess)
                {
                    TempData["Success"] = "Coupon Created Succesfully";
                    return RedirectToAction(nameof(CouponIndex));
                }
                else
                {
                    TempData["Error"] = response?.message;
                }
            }
            return View();
        }

		public async Task<IActionResult> CouponDelete(int CouponId)
        {
            var response = await _couponService.GetCouponById(CouponId);
            if (response != null && response.isSuccess)
            {
                CouponDto? couponDto = JsonConvert.DeserializeObject<CouponDto>(Convert.ToString(response.Result));
                return View(couponDto);
            }
            else
            {
                TempData["Error"] = response?.message;
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CouponDelete(CouponDto CouponDto)
        {
            var response = await _couponService.DeleteCoupon(CouponDto.CouponId);
            if (response != null && response.isSuccess)
            {
                TempData["Success"] = "Coupon Deleted Successfully";
                return RedirectToAction(nameof(CouponIndex));
            }
            else
            {
                TempData["Error"] = response?.message;
            }
            return View(CouponDto);
        }
    }
}
