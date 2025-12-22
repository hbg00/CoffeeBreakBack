using CoffeeBreakAPI.Dtos.Auth;

namespace CoffeeBreakAPI.Models.Auth
{
    public class AuthResult
    {
        public string Token { get; set; }
        public bool Success { get; set; }
        public List<string> Errors { get; set; }
        public UserDto User { get; set; }
    }
}
