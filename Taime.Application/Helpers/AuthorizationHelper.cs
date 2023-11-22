using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using Taime.Application.Constants;
using Taime.Application.Contracts.Auth;
using Taime.Application.Data.MySql.Entities;
using Taime.Application.Settings;

namespace Taime.Application.Helpers
{
    public static class AuthorizationHelper
    {
        public static string GenerateAccessToken(UserEntity userEntity, AppSettings settings)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(settings.JWTAuthorizationKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, HashIdHelper.Encode(userEntity.Id)),
                    new Claim(ClaimTypes.Name, userEntity.Name),
                    new Claim(ClaimTypes.Email, userEntity.Email),
                    new Claim(ClaimTypes.Role, userEntity.IsAdmin ? "Admin" : "Default")
                }),
                Expires = DateTime.UtcNow.AddSeconds(settings.JWTAccessTokenExpirationTime),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var accessToken = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(accessToken);
        }

        public static string GenerateRefreshToken(UserEntity userEntity, AppSettings settings)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(settings.JWTAuthorizationKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, HashIdHelper.Encode(userEntity.Id)),
                    new Claim(ClaimTypes.Role, AuthConstants.AUTH_REFRESH_ROLE)
                }),
                Expires = DateTime.UtcNow.AddSeconds(settings.JWTAccessTokenExpirationTime),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var refreshToken = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(refreshToken);
        }
    }
}