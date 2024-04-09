
using Mango.Services.OrderApi.Model.Dto;

namespace Mango.Services.OrderApi.Service
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetProducts();
    }
}
