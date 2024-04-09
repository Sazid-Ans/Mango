using AuthApi.Model;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AuthApi.Service
{
    public class TokenGenerate : ITokengenerate
    {
        private JwtOptions _jwtOptions;
        public TokenGenerate(IOptions<JwtOptions> jwtOptions)
        {
            _jwtOptions = jwtOptions.Value;   
        }
        public string TokenGenerator(ApplicationUser applicationUser , IEnumerable<string> Roles)
        {
            var TokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtOptions.Secret);

            var claimsList = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Name, applicationUser.Name),
                new Claim(JwtRegisteredClaimNames.Email, applicationUser.Email),
                new Claim(JwtRegisteredClaimNames.Sub, applicationUser.Id),
            };
            claimsList.AddRange(Roles.Select(role => new Claim(ClaimTypes.Role, role)));
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Audience = _jwtOptions.Audience,
                Issuer = _jwtOptions.Issuer,
                Subject = new ClaimsIdentity(claimsList),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = TokenHandler.CreateToken(tokenDescriptor);
            return TokenHandler.WriteToken(token);
        }
    }
}
