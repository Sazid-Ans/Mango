using MangoWeb.Models;

namespace MangoWeb.Service.Iservice
{
    public interface IProductService
    {
        Task<ResponseDto> GetProductById(int productId);
        Task<ResponseDto> GetAllProducts();
        Task<ResponseDto> UpdateProduct(ProductDto productDto);
        Task<ResponseDto> createProduct(ProductDto productDto);
        Task<ResponseDto> DeleteProduct(int productId);
    }
}
