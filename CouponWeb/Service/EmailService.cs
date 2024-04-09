using MangoWeb.Models;
using MangoWeb.Service.Iservice;
using MangoWeb.Utility;
using static MangoWeb.Utility.SD;

namespace MangoWeb.Service
{
    public class EmailService : IEmailService
    {
        private readonly IBaseService _bs;
        public string URI;
        public EmailService(IBaseService baseSerice)
        {
            _bs = baseSerice;
            URI = SD.ShoppingCartAPIbase + "/api/cart/";
        }
        public Task<ResponseDto> EmailCart(CartDto cartDto)
        {
            var uri = URI + "EmailCartRequest";
            RequestDto dto = new RequestDto { ApiType = ApiType.Post, URI = uri, Data = cartDto };
            var response = _bs.SendAsync(dto);
            return response;
        }
    }
}
