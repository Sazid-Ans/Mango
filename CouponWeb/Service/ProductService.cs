using MangoWeb.Models;
using MangoWeb.Service.Iservice;
using MangoWeb.Utility;
using static MangoWeb.Utility.SD;

namespace MangoWeb.Service
{
    public class ProductService : IProductService
    {
        private readonly IBaseService _bs;
        public string URI;

        public ProductService(IBaseService baseSerice)
        {
            _bs = baseSerice;
            URI = SD.ProductAPIbase + "/Api/Product/";
        }
        public Task<ResponseDto> createProduct(ProductDto productDto)
        {
            var uri = URI + "CreateProduct";
            RequestDto dto = new RequestDto { ApiType = ApiType.Post, URI = uri, Data = productDto };

            var response = _bs.SendAsync(dto);
            return response;
        }

        public Task<ResponseDto> DeleteProduct(int productId)
        {
            var uri = URI + "DeleteProduct?productId=" + productId;
            RequestDto dto = new RequestDto { ApiType = ApiType.Delete, URI = uri };
            var response = _bs.SendAsync(dto);
            return response;
        }

        public Task<ResponseDto> GetAllProducts()
        {
            var uri = URI + "GetAllProducts";
            RequestDto dto = new RequestDto { ApiType = ApiType.Get, URI = uri };
            var response = _bs.SendAsync(dto);
            return response;
        }

        public Task<ResponseDto> GetProductById(int productId)
        {
            var uri = URI + "GetProductByID/" + productId;
            RequestDto dto = new RequestDto { ApiType = ApiType.Get, URI = uri };
            var response = _bs.SendAsync(dto);
            return response;
        }

        public Task<ResponseDto> UpdateProduct(ProductDto productDto)
        {
            var uri = URI + "UpdateProduct/";
            RequestDto dto = new RequestDto { ApiType = ApiType.Put, URI = uri, Data = productDto };
            var response = _bs.SendAsync(dto);
            return response;
        }
    }
}
