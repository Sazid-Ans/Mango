using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Runtime.CompilerServices;
using System.Text;

namespace Mango.Services.CouponAPI.Extension
{
    public static class WebApiBuilderExtension
    {
        public static WebApplicationBuilder AppAuthenticationExt(this WebApplicationBuilder builder)
        {
            var ApiSettingsSection = builder.Configuration.GetSection("JwtOptions");
            var secret = ApiSettingsSection.GetValue<string>("Secret");
            var Issuer = ApiSettingsSection.GetValue<string>("Issuer");
            var audience = ApiSettingsSection.GetValue<string>("Audience");

            //var secret2 = builder.Configuration.GetValue<string>("JwtOptions:Secret");
            //var Issuer2 = builder.Configuration.GetValue<string>("JwtOptions:Issuer");
            //var audience2 = builder.Configuration.GetValue<string>("JwtOptions:Audience");
            var key = Encoding.ASCII.GetBytes(secret);

            builder.Services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidIssuer = Issuer,
                    ValidAudience = audience,
                    ValidateLifetime = true
                };
            });
            return builder;
        }
    }
}
