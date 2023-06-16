using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Taime.Application.Data.MySql.Entities;
using Taime.Application.Settings;

namespace Taime.Application.Helpers
{
    public static class AuthorizationHelper
    {
        public static string GenerateToken(UserEntity userEntity, AppSettings settings)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(settings.JWTAuthorizationToken);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, userEntity.Name),
                    new Claim(ClaimTypes.Role, userEntity.IsAdmin ? "Admin" : "Default"),
                }),
                Expires = DateTime.UtcNow.AddSeconds(settings.JWTTokenExpirationTime),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}