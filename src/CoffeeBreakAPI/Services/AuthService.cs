using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CoffeeBreakAPI.Constants;
using CoffeeBreakAPI.Dtos.Auth;
using CoffeeBreakAPI.Interfaces;
using CoffeeBreakAPI.Models.Auth;
using Google.Apis.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace CoffeeBreakAPI.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IConfiguration _config;

        public AuthService
            (
                UserManager<User> userManager,
                SignInManager<User> signInManager,
                IConfiguration config
            ) 
        { 
            _userManager = userManager;
            _signInManager = signInManager;
            _config = config;
        }

        public async Task<(string Token, User User)?> LoginAsync(LoginDto dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user == null) return null;

            var result = await _signInManager.CheckPasswordSignInAsync(user, dto.Password, false);
            if (!result.Succeeded) return null;

            var token = GenerateJwtToken(user);

            return (await token, user);
        }

        public async Task<(string Token, User User)?> LoginWithGoogleAsync(string idToken)
        {
            try
            {
                var settings = new GoogleJsonWebSignature.ValidationSettings
                {
                    Audience = new[] { _config["Google:ClientId"] }
                };

                var payload = await GoogleJsonWebSignature.ValidateAsync(idToken, settings);

                var email = payload.Email;

                var user = await _userManager.FindByEmailAsync(email);
                if (user == null)
                {
                    user = new User
                    {
                        UserName = email,
                        Email = email,
                        EmailConfirmed = true,
                        FirstName = payload.GivenName ?? "Google",
                        LastName = payload.FamilyName ?? "User"
                    };

                    var create = await _userManager.CreateAsync(user);
                    if (!create.Succeeded) return null;

                    await _userManager.AddToRoleAsync(user, Roles.CLIENT);
                }

                var token = await GenerateJwtToken(user);
                return (token, user);
            }
            catch
            {
                return null;
            }
        }

        public async Task<User?> RegisterAsync(RegisterDto dto)
        {
            var existingUser = await _userManager.FindByEmailAsync(dto.Email);
            if (existingUser != null)
                return null;

            var newUser = new User
            {
                UserName = dto.Email,
                Email = dto.Email,
                EmailConfirmed = true,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
            };

            var result = await _userManager.CreateAsync(newUser, dto.Password);
            if (!result.Succeeded)
                return null;

            await _userManager.AddToRoleAsync(newUser, Roles.CLIENT);

            return newUser;
        }

        public async Task<UserDto?> GetCurrentUserAsync(ClaimsPrincipal userClaims)
        {
            var userId = userClaims.FindFirstValue(JwtRegisteredClaimNames.Sub);
            if (userId == null)
                return null;

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return null;

            return new UserDto
            {
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber ?? ""
            };
        }

        private async Task<string> GenerateJwtToken(User user)
        {
            var userRoles = await _userManager.GetRolesAsync(user);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Name, $"{user.FirstName} {user.LastName}")
            };

            foreach (var role in userRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddDays(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
