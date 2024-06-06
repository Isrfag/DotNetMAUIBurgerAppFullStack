using Burger.Shared.Dtos;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BurgerAPI.Api.Services
{
    public class TokenService
    {
        private readonly IConfiguration _configuration;

        public TokenService (IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public static TokenValidationParameters GetTokenValidationParameters(IConfiguration config)
        {
            return new Microsoft.IdentityModel.Tokens.TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = config["Jwt:Issuer"],
                IssuerSigningKey = GetSecurityKey(config),
            };
        }

        public string GenerateJwt(LoggedInUser user )
        {
             // appsettings.json
            string? issuer = _configuration["Jwt:Issuer"];
            string? audience = _configuration["Jwt:Audience"];
            int expirerMinutes = Convert.ToInt32 (_configuration["Jwt:ExpireInMinute"]);

            Claim[] claims = [
                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Email,user.Email),
                new Claim(ClaimTypes.StreetAddress, user.Address),
               ];


            SymmetricSecurityKey securityKey = GetSecurityKey(_configuration);

            SigningCredentials credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken token = new JwtSecurityToken(
                                            issuer: issuer,
                                            audience: audience,
                                            claims: claims,
                                            expires: DateTime.Now.AddMinutes(expirerMinutes),
                                            signingCredentials: credentials
                                        );
            string finalJwt = new JwtSecurityTokenHandler().WriteToken(token);
            return finalJwt;
        }

        private static SymmetricSecurityKey GetSecurityKey (IConfiguration config)
        {
            string? secretKey = config["Jwt:SecretKey"];
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey!));
            return securityKey;
        }
    }
}
