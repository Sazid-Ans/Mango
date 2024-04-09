using AuthApi.Data;
using AuthApi.Model;
using AuthApi.Model.Dto;
using Microsoft.AspNetCore.Identity;

namespace AuthApi.Service
{
    public class AuthService : IAuthService
    {
        private readonly AuthDbContext _authDb;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ITokengenerate _tokengenerate;
        public AuthService(AuthDbContext authDb, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, ITokengenerate tokengenerate)
        {
            _authDb = authDb;
            _userManager = userManager;
            _roleManager = roleManager;
            _tokengenerate = tokengenerate;
        }

        public async Task<bool> AssignRole(string email, string roleName)
        {
            var user = _authDb.Users.FirstOrDefault(u => u.Email.ToLower() == email.ToLower());
            if (user != null)
            {
                if (!_roleManager.RoleExistsAsync(roleName).GetAwaiter().GetResult())
                {
                    _roleManager.CreateAsync(new IdentityRole(roleName)).GetAwaiter().GetResult();
                }
                await _userManager.AddToRoleAsync(user, roleName);
                return true;
            }
            return false;
        }

        public async Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto)
        {
            var user = _authDb.Users.FirstOrDefault(u => u.UserName.ToLower() == loginRequestDto.UserName.ToLower());
            bool isvalid = await _userManager.CheckPasswordAsync(user, loginRequestDto.password);
            if (user == null || isvalid == false)
            {
                return new LoginResponseDto()
                {
                    User = null,
                    Token = ""
                };
            }

            // if user was found generate jwt Token
            var Roles = await _userManager.GetRolesAsync(user); 
            var token = _tokengenerate.TokenGenerator(user, Roles);
            var userDto = new UserDto()
            {
                UserEmail = user.Email,
                UserName = user.UserName,
                UserPhoneNumber = user.PhoneNumber,
                UserID = user.Id
            };
            LoginResponseDto loginResponseDto = new()
            {
                User = userDto,
                Token = token
            };
            return loginResponseDto;
        }

        public async Task<string> Register(RegistrationRequestDto registrationRequestDto)
        {
            ApplicationUser applicationUser = new()
            {
                UserName = registrationRequestDto.UserEmail,
                Email = registrationRequestDto.UserEmail,
                NormalizedEmail = registrationRequestDto.UserEmail.ToUpper(),
                Name = registrationRequestDto.UserName,
                PhoneNumber = registrationRequestDto.UserPhoneNumber
            };
            try
            {
                var result = await _userManager.CreateAsync(applicationUser, registrationRequestDto.password);

                if (result.Succeeded)
                {
                    var userToReturn = _authDb.Users.First(u => u.UserName == registrationRequestDto.UserEmail);

                    var userDto = new UserDto()
                    {
                        UserName = userToReturn.UserName,
                        UserEmail = userToReturn.Email,
                        UserPhoneNumber = userToReturn.PhoneNumber
                    };
                    return "";
                }
                else
                {
                    return result.Errors.First().Description;
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            return "Error while registration";
        }
    }
}
