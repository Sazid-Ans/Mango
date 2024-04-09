using MangoWeb.Models;

namespace MangoWeb.Service.Iservice
{
    public interface IEmailService
    {
        Task<ResponseDto> EmailCart(CartDto cartDto); 
    }
}
