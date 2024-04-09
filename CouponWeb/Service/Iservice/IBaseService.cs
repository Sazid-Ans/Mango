using MangoWeb.Models;

namespace MangoWeb.Service.Iservice
{
    public interface IBaseService
    {
        Task<ResponseDto?> SendAsync(RequestDto request, bool withBearer = true);
    }
}
