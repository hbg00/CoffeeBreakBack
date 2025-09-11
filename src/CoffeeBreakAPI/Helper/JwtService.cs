using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using CoffeeBreakAPI.Model;
using Microsoft.IdentityModel.Tokens;

namespace CoffeeBreakAPI.Helper
{
    public class JwtService
    {
        public static string GenerateJwtToken(User user, IConfiguration cfg)
        {
            var jwtSecret = cfg["JWT__Secret"]!;
            var jwtIssuer = cfg["JWT__Issuer"] ?? "my-app";
            var jwtAudience = cfg["JWT__Audience"] ?? "my-app";
            var expiryMinutes = int.Parse(cfg["JWT__ExpiryMinutes"] ?? "15");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("role", user.Role)
            };

            var token = new JwtSecurityToken(
                issuer: jwtIssuer,
                audience: jwtAudience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(expiryMinutes),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public static string CreateRandomToken(int size = 64)
        {
            var bytes = RandomNumberGenerator.GetBytes(size);
            return Convert.ToBase64String(bytes);
        }
    }
}
