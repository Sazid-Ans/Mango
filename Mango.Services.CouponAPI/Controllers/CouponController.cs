using AutoMapper;
using Azure;
using Mango.Services.CouponAPI.Data;
using Mango.Services.CouponAPI.Models;
using Mango.Services.CouponAPI.Models.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace Mango.Services.CouponAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CouponController : ControllerBase
    {
        private AppDbContext _db;
        public ResponseDTO _response = new();
        private IMapper _mapper;
        public CouponController(AppDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }
        [HttpGet]
        [Route("getAllCoupon")]
        public async Task<ActionResult<ResponseDTO>> GetCoupons()
        {
            var coupon = _db.Coupons.ToList();
            var couponDTO = _mapper.Map<IEnumerable<CouponDto>>(coupon);
            var service = new Stripe.CouponService();
            var t2 = service.ListAsync();
            try
            {
                if (coupon != null)
                {
                    _response = new ResponseDTO(couponDTO, true, "Sucsess");
                }
            }
            catch (Exception ex)
            {
                _response = new ResponseDTO(couponDTO, false, ex.Message);
            }

            return _response;
        }
        [HttpGet]
        //[Authorize]
        [Route("getCoupon/{id}")] //https://localhost:7001/getCoupon/2
        public async Task<ActionResult<ResponseDTO>> GetCoupon(int id) //if the paramter is of simple type then the [Default Type] web api tries to get value from either query parameter
                                                                       // or route data. we can also use explicitly FromBody
                                                                       //if the paramter is of complex type then the web api [Default Type] tries to get value from From RequestBody
                                                                       // or route data. we can also use explicitly FromURI
        {
            try
            {
                if (id == 0)
                {
                    _response = new ResponseDTO(null, false, "Id cannot be zero or empty");
                }
                Coupon coupon = _db.Coupons.First(x => x.CouponId == id); //FirstOrDefault will never throw null refrence excpetion 
                if (coupon != null)
                {
                    _response = new ResponseDTO(coupon, true, "Sucsess");
                }
            }
            catch (Exception ex)
            {
                _response = new ResponseDTO(null, false, ex.Message);
            }

            return _response;
        }

        [HttpGet]
        [Route("getCouponByCode/{code}")]
        public async Task<ActionResult<ResponseDTO>> getCouponByCode(string code)
        {
            try
            {
                if (code == "")
                {
                    _response = new ResponseDTO(null, false, "code cannot be empty");
                }
                Coupon coupon = _db.Coupons.First(x => x.CouponCode.ToLower() == code.ToLower());

                if (coupon != null)
                {
                    _response = new ResponseDTO(coupon, true, "Sucsess");
                }
            }
            catch (Exception ex)
            {
                _response = new ResponseDTO(null, false, ex.Message);
            }

            return _response;
        }
        [HttpPost]
        [Route("CreateCoupon")]
        //[Authorize(Roles = "ADMIN")]
        public async Task<ActionResult<ResponseDTO>> CreateCouponByCode(CouponDto couponDto) //default FromBody
        {
            try
            {
                if (couponDto.CouponCode == "")
                {
                    _response = new ResponseDTO(null, false, "code cannot be empty");
                }
                var coupon = _mapper.Map<Coupon>(couponDto);
                await _db.Coupons.AddAsync(coupon);
                _db.SaveChanges();
                var options = new Stripe.CouponCreateOptions
                {
                    AmountOff = (long)(couponDto.DiscountAmount * 100),
                    Name = couponDto.CouponCode,
                    Currency = "usd",
                    Id = couponDto.CouponCode,
                };
                var service = new Stripe.CouponService();
                var t = service.Create(options);
                if (couponDto != null)
                {
                    return Created("abc", new ResponseDTO(_mapper.Map<CouponDto>(coupon), true, "Created"));
                }
            }
            catch (Exception ex)
            {
                _response = new ResponseDTO(null, false, ex.Message);
            }

            return _response;
        }

        [HttpPut]
        [Route("UpdateCoupon")]
        [Authorize(Roles = "ADMIN")]
        public async Task<ActionResult<ResponseDTO>> UpdateCouponByCode(CouponDto couponDto) //default FromBody
        {
            try
            {
                 Coupon coupon = _mapper.Map<Coupon>(couponDto);
                _db.Update(coupon);
                _db.SaveChanges();
                return Ok(new ResponseDTO(_mapper.Map<CouponDto>(coupon), true, "Updated"));
            }
            catch (Exception ex)
            {
                _response = new ResponseDTO(null, false, ex.Message);
            }

            return _response;
        }

        [HttpDelete]
        [Route("DeleteCoupun")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ResponseDTO>> DeleteCoupon(int id) //default FromURI
        {
            try
            {
                var coupon = await _db.Coupons.FirstOrDefaultAsync(x => x.CouponId == id);
                if (coupon == null)
                {
                    _response = new ResponseDTO(null, false, "Coupon doesnot exits");
                }
                else
                {
                    _db.Coupons.Remove(coupon);
                    _db.SaveChanges();
                    var service = new Stripe.CouponService();
                    service.Delete(coupon.CouponCode);
                    _response = new ResponseDTO(null, true, "Deleted");
                }
            }
            catch (Exception ex)
            {
                _response = new ResponseDTO(null, false, ex.Message);
            }

            return _response;
        }
    }
}
