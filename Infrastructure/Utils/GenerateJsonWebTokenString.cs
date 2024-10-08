using Application.Commons;
using Domain.Entities;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Security.Claims;
using System.Text;
using System.Security.Cryptography;

namespace Infrastructure.Utils
{
    public static class GenerateJsonWebTokenString
    {
        private static byte[] GetSecureKey(string secretKey)
        {
            var keyBytes = Encoding.UTF8.GetBytes(secretKey);
            if (keyBytes.Length < 32)
            {
                throw new ArgumentException("SecretKey must be at least 32 bytes.");
            }
            return keyBytes;
        }

        public static string GenerateJsonWebToken(this Account user, AppConfiguration appSettingConfiguration)
        {
            var secretKey = appSettingConfiguration.JWTSection.SecretKey;
            var keyBytes = GetSecureKey(secretKey);
            var securityKey = new SymmetricSecurityKey(keyBytes);
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim("Id", user.AccountId.ToString()),
                new Claim("Email", user.Email),
                new Claim(ClaimTypes.Role, user.Role),
            };

            var token = new JwtSecurityToken(
                issuer: appSettingConfiguration.JWTSection.Issuer,
                audience: appSettingConfiguration.JWTSection.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddHours(3), // You can modify the expiration time here
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public static string GenerateAccessToken(Account user, AppConfiguration appSettingConfiguration)
        {
            var secretKey = appSettingConfiguration.JWTSection.SecretKey;
            var keyBytes = GetSecureKey(secretKey);
            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, user.AccountId.ToString()),
                    new Claim(ClaimTypes.Role, user.Role)
                }),
                Expires = DateTime.UtcNow.AddMinutes(15), // Access token expiration (configurable)
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(keyBytes), SecurityAlgorithms.HmacSha256Signature),
                Issuer = appSettingConfiguration.JWTSection.Issuer,
                Audience = appSettingConfiguration.JWTSection.Audience
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public static string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }
    }
}
