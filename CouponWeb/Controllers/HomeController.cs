using IdentityModel;
using MangoWeb.BaseService;
using MangoWeb.Models;
using MangoWeb.Service.Iservice;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;
using Newtonsoft.Json;
using System.Diagnostics;

namespace MangoWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductService _ProductService;
        private readonly ICartService _cartService;

        public HomeController(ILogger<HomeController> logger, IProductService productService, ICartService cartService)
        {
            _logger = logger;
            _ProductService = productService;
            _cartService = cartService;
        }

        public async Task<ActionResult<IEnumerable<ProductDto>>> Index()
        {
            List<ProductDto> Products = new();
            var response = await _ProductService.GetAllProducts();
            if (response != null && response.isSuccess)
            {
                Products = JsonConvert.DeserializeObject<List<ProductDto>>(Convert.ToString(response.Result));
            }
            else
            {
                TempData["Error"] = response?.message;
            }
            return View(Products);
        }
        [Authorize]
        public async Task<ActionResult<ProductDto>> ProductDetails(int ProdID)
        {
            ProductDto Product = new();
            var response = await _ProductService.GetProductById(ProdID);
            if (response != null && response.isSuccess)
            {
                Product = JsonConvert.DeserializeObject<ProductDto>(Convert.ToString(response.Result));
            }
            else
            {
                TempData["Error"] = response?.message;
            }
            return View(Product);
        }
        [Authorize]
        [HttpPost]
        [ActionName("AddToCart")]
        public async Task<ActionResult<ProductDto>> ProductDetails(ProductDto productDto)
        {
            CartDto cartDto = new CartDto()
            {
                CartHeader = new CartHeaderDto()
                {
                    UserId = User.Claims.Where(u => u.Type == JwtClaimTypes.Subject)?.FirstOrDefault()?.Value
                }
            };
            cartDetailsDto cartDetails = new cartDetailsDto()
            {
                Count = productDto.count,
                ProductId = productDto.ProductId
            };
            List<cartDetailsDto> cartDetailsDtos = new() { cartDetails };
            cartDto.CartDetails = cartDetailsDtos;
            var response = await _cartService.UpsertCartAsync(cartDto);
            if (response != null && response.isSuccess)
            {
                TempData["Success"] = "Item has been Added to the Shopping cart";

                return RedirectToAction(nameof(Index));
            }
            else
            {
                TempData["Error"] = response?.message;
            }
            return View(productDto);
        }
        public IActionResult Privacy()
        {
            return View();
        }

    }
}