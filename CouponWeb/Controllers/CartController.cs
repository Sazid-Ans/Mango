using MangoWeb.Models;
using MangoWeb.Service;
using MangoWeb.Service.Iservice;
using MangoWeb.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;
using Newtonsoft.Json;

namespace MangoWeb.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService _cartService;
        private readonly IEmailService _EmailService;
        private readonly IOrderService _OrderService;
        public CartController(ICartService cartService, IEmailService emailService, IOrderService orderService)
        {
            _cartService = cartService;
            _EmailService = emailService;
            _OrderService = orderService;
        }
        [Authorize]
        public async Task<IActionResult> CartIndex()
        {
            var userId = User.Claims.Where(u => u.Type == JwtRegisteredClaimNames.Sub)?.FirstOrDefault()?.Value;
            var response = await _cartService.GetCartByUserIdAsnyc(userId);
            if (response != null && response.isSuccess)
            {
                CartDto cartDto = JsonConvert.DeserializeObject<CartDto>(Convert.ToString(response.Result));
                return View(cartDto);
            }
            return View(new CartDto());
            //return View(await LoadcartDtobasedOnLoggedInUser());
        }
        [Authorize]
        public async Task<IActionResult> CheckOut()
        {
            return View(await LoadcartDtobasedOnLoggedInUser());
        }
        [Authorize]
        [HttpPost]
        [ActionName("CheckOut")]
        public async Task<IActionResult> CheckOut(CartDto cartDto)
        {
            CartDto cart = await LoadcartDtobasedOnLoggedInUser();
            cart.CartHeader.Phone = cartDto.CartHeader.Phone;
            cart.CartHeader.Email = cartDto.CartHeader.Email;
            cart.CartHeader.FullName = cartDto.CartHeader.FullName;
            var response = await _OrderService.CreateOrder(cart);
            OrderHeaderDto? orderHeaderDto = JsonConvert.DeserializeObject<OrderHeaderDto>(Convert.ToString(response.Result));

            if (response != null && response.isSuccess)
            {
                //get stripe session and redirect to stripe to place order
                //
                var domain = Request.Scheme + "://" + Request.Host.Value + "/";

                StripeRequestDto stripeRequestDto = new()
                {
                    ApprovedUrl = domain + "Confirm/" + orderHeaderDto.OrderHeaderId,
                    CancelUrl = domain + "cart/checkout",
                    OrderHeader = orderHeaderDto
                };

                var stripeResponse = await _OrderService.CreateStripeSession(stripeRequestDto);
                StripeRequestDto stripeResponseResult = JsonConvert.DeserializeObject<StripeRequestDto>
                                            (Convert.ToString(stripeResponse.Result));
                Response.Headers.Add("Location", stripeResponseResult.StripeSessionUrl);
                return new StatusCodeResult(303);
            }
            return View();
        }

        [HttpGet("Confirm/{orderId}")] //There is no Route attribute in web mvc , therefore route will be overridden to Localhost/Confirm/{orderId}
                                       // same with [Route] attribute. 
        public async Task<IActionResult> Confirmation(int orderId)
        {
            ResponseDto? response = await _OrderService.ValidateStripeSession(orderId);
            if (response != null & response.isSuccess)
            {
                OrderHeaderDto orderHeader = JsonConvert.DeserializeObject<OrderHeaderDto>(Convert.ToString(response.Result));
                if (orderHeader.Status == SD.Status_Approved)
                {
                    return View(orderId);
                }
            }
            //redirect to some error page based on status
            return View(orderId);
        }

        private async Task<CartDto> LoadcartDtobasedOnLoggedInUser()
        {
            var userId = User.Claims.Where(u => u.Type == JwtRegisteredClaimNames.Sub)?.FirstOrDefault()?.Value;
            ResponseDto? response = await _cartService.GetCartByUserIdAsnyc(userId);
            if (response != null && response.isSuccess)
            {
                CartDto? cartDto = JsonConvert.DeserializeObject<CartDto?>(Convert.ToString(response.Result));
                return cartDto;
            }
            return new CartDto();
        }

        public async Task<IActionResult> RemoveFromCart(int cartDeatilsId)
        {
            var userId = User.Claims.Where(u => u.Type == JwtRegisteredClaimNames.Sub)?.FirstOrDefault()?.Value;

            var response = await _cartService.RemoveFromCartAsync(cartDeatilsId);
            if (response != null && response.isSuccess)
            {
                TempData["Success"] = "cart updated Successfully";
                return RedirectToAction(nameof(CartIndex));
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ApplyCoupon(CartDto cartDto)
        {
            var response = await _cartService.ApplyCouponAsync(cartDto);
            if (response != null && response.isSuccess)
            {
                TempData["Success"] = "Coupon Applied Successfully";
                return RedirectToAction(nameof(CartIndex));
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> EmailCart(CartDto cartDto)
        {
            var response = await _EmailService.EmailCart(cartDto);
            if (response != null && response.isSuccess)
            {
                TempData["Success"] = "Email will be processed and sent shortly";
                return RedirectToAction(nameof(CartIndex));
            }
            //return View();
            return RedirectToAction(nameof(CartIndex));
        }
        public async Task<IActionResult> RemoveCoupon(CartDto cartDto)
        {
            cartDto.CartHeader.CouponCode = "";
            var response = await _cartService.ApplyCouponAsync(cartDto);
            if (response != null && response.isSuccess)
            {
                TempData["Success"] = "Coupon Removed Successfully";
                return RedirectToAction(nameof(CartIndex));
            }
            return View();
        }
    }
}
