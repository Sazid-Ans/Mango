using MangoWeb.Models;

namespace MangoWeb.Service.Iservice
{
    public interface IAuthService
    {
        Task<ResponseDto> Login(LoginRequestDto loginRequestDto);
        Task<ResponseDto> Register(RegistrationRequestDto registrationRequestDto);
        Task<ResponseDto> AssignRole(RegistrationRequestDto registrationRequestDto);
    }
}
