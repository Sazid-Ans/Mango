using MangoWeb.Models;
using MangoWeb.Service.Iservice;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ProductWeb.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _ProductService;
        public ProductController(IProductService ProductService)
        {
            _ProductService = ProductService;
        }
        public async Task<ActionResult<IEnumerable<ProductDto>>> ProductIndex()
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

        public async Task<IActionResult> ProductCreate()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ProductCreate(ProductDto Product)
        {
            if (ModelState.IsValid)
            {
                var response = await _ProductService.createProduct(Product);
                if (response != null && response.isSuccess)
                {
                    TempData["Success"] = "Product Created Succesfully";
                    return RedirectToAction(nameof(ProductIndex));
                }
                else
                {
                    TempData["Error"] = response?.message;
                }
            }
            return View();
        }
        public async Task<IActionResult> ProductDelete(int ProductId)
        {
            var response = await _ProductService.GetProductById(ProductId);
            if (response != null && response.isSuccess)
            {
                var ProductDto = JsonConvert.DeserializeObject<ProductDto>(Convert.ToString(response.Result));
                return View(ProductDto);
            }
            else
            {
                TempData["Error"] = response?.message;
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ProductDelete(ProductDto ProductDto)
        {
            var response = await _ProductService.DeleteProduct(ProductDto.ProductId);
            if (response != null && response.isSuccess)
            {
                TempData["Success"] = "Product Deleted Successfully";
                return RedirectToAction(nameof(ProductIndex));
            }
            else
            {
                TempData["Error"] = response?.message;
            }
            return View(ProductDto);
        }
		public async Task<IActionResult> ProductEdit(int productId)
		{
			ResponseDto? response = await _ProductService.GetProductById(productId);

			if (response != null && response.isSuccess)
			{
				ProductDto? model = JsonConvert.DeserializeObject<ProductDto>(Convert.ToString(response.Result));
				return View(model);
			}
			else
			{
				TempData["error"] = response?.message;
			}
			return NotFound();
		}

		[HttpPost]
		public async Task<IActionResult> ProductEdit(ProductDto productDto)
		{
			if (ModelState.IsValid)
			{
				ResponseDto? response = await _ProductService.UpdateProduct(productDto);

				if (response != null && response.isSuccess)
				{
					TempData["Success"] = "Product updated successfully";
					return RedirectToAction(nameof(ProductIndex));
				}
				else
				{
					TempData["Error"] = response?.message;
				}
			}
			return View(productDto);
		}
	}
}
