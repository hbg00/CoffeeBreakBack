using System.Security.Claims;
using CoffeeBreakAPI.Dtos.Auth;
using CoffeeBreakAPI.Models.Auth;

namespace CoffeeBreakAPI.Interfaces
{
    public interface IAuthService
    {
        Task<(string Token, User User)?> LoginAsync(LoginDto dto);
        Task<User?> RegisterAsync(RegisterDto dto);
        Task<UserDto?> GetCurrentUserAsync(ClaimsPrincipal userClaims);
        Task<(string Token, User User)?> LoginWithGoogleAsync(string idToken);
    }
}
