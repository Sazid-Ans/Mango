using AuthApi.Data;
using AuthApi.Model.Dto;
using AuthApi.Service;
using Mango.Azure.ServiceBus;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace AuthApi.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthApiController : ControllerBase
    {
        private readonly IAuthService _authService;
        protected ResponseDTO _responseDTO;
        protected IAzureMessageBus _AzureMessageBus;
        protected IConfiguration _config;
        public AuthApiController(IAuthService authService, IAzureMessageBus azureMessageBus, IConfiguration config)
        {
            _authService = authService;
            _responseDTO = new();
            _AzureMessageBus = azureMessageBus;
            _config = config;
        }
        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegistrationRequestDto registrationRequestDto)
        {
            var errorMessage = await _authService.Register(registrationRequestDto);
            if (!errorMessage.IsNullOrEmpty())
            {
                _responseDTO.isSuccess = false;
                _responseDTO.message = errorMessage;
                return BadRequest(_responseDTO);
            }
            
            //await _AzureMessageBus.PublishMessage(registrationRequestDto.UserEmail,_config.GetValue<string>("TopicAndQueueName:RegisterUserQueue"));
            return Ok(_responseDTO);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginRequestDto loginRequestDto)
        {
            var loginresponse = await _authService.Login(loginRequestDto);
            if(loginresponse.User == null)
            {
                _responseDTO.isSuccess = false;
                _responseDTO.message = "username or Password is Incorrect";
                return BadRequest(_responseDTO);
            }
            _responseDTO.Result = loginresponse;
            return Ok(_responseDTO);
        }
        [HttpPost("AssignRole")]
        public async Task<IActionResult> AssignRole(RegistrationRequestDto registrationRequestDto)
        {
            var AssignRoleSuccess = await _authService.AssignRole(registrationRequestDto.UserEmail , registrationRequestDto.UserRole);
            if (!AssignRoleSuccess)
            {
                _responseDTO.isSuccess = false;
                _responseDTO.message = "Assign Role failed";
                return BadRequest(_responseDTO);
            }
            return Ok(_responseDTO);
        }
    }
}
