using AutoMapper;
using Azure;
using Mango.Services.ProductApi.Data;
using Mango.Services.ProductApi.Model.Core;
using Mango.Services.ProductApi.Model.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Mango.Services.ProductApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductController : ControllerBase
    {
        private readonly AppDbContext _db;
        private readonly IMapper _mapper;
        private ResponseDTO _response = new();

        public ProductController(AppDbContext db, IMapper map)
        {
            _db = db;
            _mapper = map;
        }

        [HttpGet("GetAllProducts")]
        public async Task<ActionResult<ResponseDTO>> GetAllProducts()
        {
            IEnumerable<Product> products =await _db.Products.ToListAsync();
            var ProductDto = _mapper.Map<IEnumerable<ProductDto>>(products);
            try
            {
                if (ProductDto != null)
                {
                    _response = new ResponseDTO(ProductDto, true, "Sucsess");
                }
            }
            catch (Exception ex)
            {
                _response = new ResponseDTO(ProductDto, false, ex.Message);
            }

            return _response;
        }
        [HttpGet("GetProductByID/{productId}")]  // if paramter GetProductByID/{Id} name are different then two inputs will be created and if we don't
                                                // provide for {id} then error message "Sequence contains no elements" will be thrown 
        public async Task<ActionResult<ResponseDTO>> GetProductByID(int productId)
        {
            try
            {
                if (productId == 0)
                {
                    _response = new ResponseDTO(null, false, "Id cannot be zero or empty");
                }
                Product product = _db.Products.First(x => x.ProductId == productId); //FirstOrDefault will never throw null refrence excpetion 
                if (product != null)
                {
                    _response = new ResponseDTO(product, true, "Sucsess");
                }
            }
            catch (Exception ex)
            {
                _response = new ResponseDTO(null, false, ex.Message);
            }

            return _response;
        }
        [HttpPut("UpdateProduct")]
        [Authorize(Roles ="Admin")]
        public async Task<ActionResult<ResponseDTO>> UpdateProduct(ProductDto productDto)
        {
            try
            {
                Product product = _mapper.Map<Product>(productDto);
                _db.Update(product);
                _db.SaveChanges();
                return Ok(new ResponseDTO(_mapper.Map<Product>(productDto), true, "Updated"));
            }
            catch (Exception ex)
            {
                _response = new ResponseDTO(null, false, ex.Message);
            }

            return _response;
        }
        [HttpPost("CreateProduct")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ResponseDTO>> CreateProduct(ProductDto productDto)
        {
            try
            {
                var product = _mapper.Map<Product>(productDto);
                await _db.Products.AddAsync(product);
                _db.SaveChanges();
                if (product != null)
                {
                    return Created("abc", new ResponseDTO(_mapper.Map<ProductDto>(product), true, "Created"));
                }
            }
            catch (Exception ex)
            {
                _response = new ResponseDTO(null, false, ex.Message);
            }

            return _response;
        }
        [HttpDelete("DeleteProduct")]  // if paramter DeleteProduct/{productid} name are same then one input will be created in swagger
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ResponseDTO>> DeleteProduct(int productId)
        {
            try
            {
                var product = await _db.Products.FirstOrDefaultAsync(x => x.ProductId == productId);
                if (product == null)
                {
                    _response = new ResponseDTO(null, false, "Coupon doesnot exits");
                }
                else
                {
                    _db.Products.Remove(product);
                    _db.SaveChanges();
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
