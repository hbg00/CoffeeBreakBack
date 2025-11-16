using CoffeeBreakAPI.Dtos;
using CoffeeBreakAPI.Models;

namespace CoffeeBreakAPI.Interfaces
{
    public interface IAuthService
    {
        Task<string?> LoginAsync(LoginDto dto);
        Task <User?> RegisterAsync(RegisterDto dto);
    }
}
