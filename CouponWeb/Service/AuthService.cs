using MangoWeb.Models;
using MangoWeb.Service.Iservice;
using MangoWeb.Utility;

namespace MangoWeb.BaseService
{
    public class AuthService : IAuthService
    {

        private readonly IBaseService _bs;
        public string URI;
        public AuthService(IBaseService baseSerice)
        {
            _bs = baseSerice;
            URI = SD.AuthAPIbase + "/api/auth/";
        }

        public async Task<ResponseDto> AssignRole(RegistrationRequestDto registrationRequestDto)
        {
            var uri = URI + "AssignRole";
            var data = registrationRequestDto;
            var requestDto = new RequestDto()
            {
                URI = uri,
                Data = data,
                ApiType = SD.ApiType.Post
            };
            return await _bs.SendAsync(requestDto);
        }

        public async Task<ResponseDto> Login(LoginRequestDto loginRequestDto)
        {
            var uri = URI + "Login";
            var data = loginRequestDto;
            var requestDto = new RequestDto()
            {
                URI = uri,
                Data = data,
                ApiType = SD.ApiType.Post
            };
            return await _bs.SendAsync(requestDto , withBearer:false);
        }

        public async Task<ResponseDto> Register(RegistrationRequestDto registrationRequestDto)
        {
            var uri = URI + "Register";
            var data = registrationRequestDto;
            var requestDto = new RequestDto()
            {
                URI = uri,
                Data = data,
                ApiType = SD.ApiType.Post
            };
          return await _bs.SendAsync(requestDto, withBearer: false);

        }
    }
}
