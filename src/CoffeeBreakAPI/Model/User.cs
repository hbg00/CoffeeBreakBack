namespace CoffeeBreakAPI.Model
{
    public class User
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Email { get; set; } = default!;
        public string? PasswordHash { get; set; }
        public string? OAuthProvider { get; set; }
        public string? ProviderId { get; set; }
        public string? GivenName { get; set; }
        public string? FamilyName { get; set; }
        public string Role { get; set; } = "User";
    }

}
