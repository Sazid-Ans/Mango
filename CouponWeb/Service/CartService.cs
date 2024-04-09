using MangoWeb.Models;
using MangoWeb.Service.Iservice;
using MangoWeb.Utility;
using Newtonsoft.Json.Linq;

namespace MangoWeb.Service
{
    public class CartService : ICartService
    {
        private readonly IBaseService _bs;
        public string uri;
        public CartService(IBaseService bs)
        {
            _bs = bs;
            this.uri = SD.ShoppingCartAPIbase + "/Api/Cart/"; ;
        }
        public async Task<ResponseDto?> ApplyCouponAsync(CartDto cartDto)
        {
            RequestDto request = new RequestDto()
            {
                Data = cartDto,
                ApiType = SD.ApiType.Post,
                URI = uri + "ApplyCoupon",
            };
           return await _bs.SendAsync(request);
        }

        public Task<ResponseDto?> EmailCart(CartDto cartDto)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseDto?> GetCartByUserIdAsnyc(string userId)
        {
            RequestDto request = new RequestDto()
            {
                ApiType = SD.ApiType.Get,
                URI = uri + "GetCart/"+ userId
            };
            return await _bs.SendAsync(request);
        }

        public async Task<ResponseDto?> RemoveFromCartAsync(int cartDetailsId)
        {
            RequestDto request = new RequestDto()
            {
                Data = cartDetailsId,
                ApiType = SD.ApiType.Post,
                URI = uri + "RemoveCart"
            };
            return await _bs.SendAsync(request);
        }

        public async Task<ResponseDto?> UpsertCartAsync(CartDto cartDto)
        {
            RequestDto request = new RequestDto()
            {
                Data = cartDto,
                ApiType = SD.ApiType.Put,
                URI = uri + "CartUpsert"
            };
            return await _bs.SendAsync(request);
        }
    }
}
