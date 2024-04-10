using MangoWeb.Models;
using MangoWeb.Service.Iservice;
using MangoWeb.Utility;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using static System.Net.Mime.MediaTypeNames;

namespace MangoWeb.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;
        private readonly ITokenProvider _tokenProvider;

        public AuthController(IAuthService authService, ITokenProvider tokenProvider)
        {
            _authService = authService;
            _tokenProvider = tokenProvider;
        }

        [HttpGet]
        public IActionResult Login()
        {
            LoginRequestDto loginRequestDto = new LoginRequestDto();
            return View(loginRequestDto);
        }
        [HttpGet]
        public IActionResult Register()
        {
            var RoleListItem = new List<SelectListItem>()
            {
               new SelectListItem() { Text = SD.RoleAdmin, Value = SD.RoleAdmin } ,
               new SelectListItem() { Text = SD.RoleCustomer, Value = SD.RoleCustomer }
            };
            ViewBag.RoleList = RoleListItem;
            RegistrationRequestDto registrationRequestDto = new RegistrationRequestDto() ;
            return View(registrationRequestDto);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginRequestDto loginRequestDto)
        {
            var response = await _authService.Login(loginRequestDto);
            if (response.isSuccess && response != null)
            {
                var LoginResponse = JsonConvert.DeserializeObject<LoginResponseDto>(Convert.ToString(response.Result));
                await SignInUser(LoginResponse);
                _tokenProvider.SetToken(LoginResponse.Token);

                return RedirectToAction("Index", "Home");
            }
            ModelState.AddModelError("CustomLoginError",response.title + response.message);
            TempData["Error"] = response.title + response.message;
            return View(loginRequestDto);
        }
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            _tokenProvider.ClearToken();
            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegistrationRequestDto registrationRequestDto)
        {
            //var selectListItem = new List<SelectListItem>();
            //selectListItem.Add(new SelectListItem() { Text = "RoleAdmin", Value = SD.RoleAdmin });
            //selectListItem.Add(new SelectListItem() { Text = "RoleCust", Value = SD.RoleCustomer });

            var response = await _authService.Register(registrationRequestDto);
            if (response != null && response.isSuccess)
            {
                if (string.IsNullOrEmpty(registrationRequestDto.UserRole))
                {
                    registrationRequestDto.UserRole = SD.RoleCustomer;
                }
                var assignRole = await _authService.AssignRole(registrationRequestDto);
                if (assignRole != null && assignRole.isSuccess)
                {
                    TempData["Success"] = "Registration Succesfully";
                    return RedirectToAction(nameof(Login));
                }
            }
            else
            {
                TempData["Error"] = response.message;
            }
            var RoleListItem = new List<SelectListItem>()
            {
               new SelectListItem() { Text = SD.RoleAdmin, Value = SD.RoleAdmin } ,
               new SelectListItem() { Text = SD.RoleCustomer, Value = SD.RoleCustomer }
            };
            ViewBag.RoleList = RoleListItem;
            return View(registrationRequestDto);
        }

        private async Task SignInUser(LoginResponseDto loginResponse)
        {
            var Handler = new JwtSecurityTokenHandler();
            var JwtToken = Handler.ReadJwtToken(loginResponse.Token);
            var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
            identity.AddClaim(new Claim(JwtRegisteredClaimNames.Email, JwtToken.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Email).Value));
            identity.AddClaim(new Claim(JwtRegisteredClaimNames.Sub, JwtToken.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Sub).Value));
            identity.AddClaim(new Claim(JwtRegisteredClaimNames.Name, JwtToken.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Name).Value));

            identity.AddClaim(new Claim(ClaimTypes.Name, JwtToken.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Email).Value));

            foreach (var role in JwtToken.Claims.Where(r => r.Type == "role"))
            {
                identity.AddClaim(new Claim(ClaimTypes.Role, role.Value));
            }
            identity.AddClaims(new List<Claim> {new Claim(ClaimTypes.Role , "role") });

            var principle = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principle);
        }
    }
}
