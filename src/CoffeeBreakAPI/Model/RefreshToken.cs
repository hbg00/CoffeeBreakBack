namespace CoffeeBreakAPI.Model
{
    public class RefreshToken
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Token { get; set; } = default!;
        public Guid UserId { get; set; }
        public DateTime ExpiresAt { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public bool Revoked { get; set; } = false;
        public string? ReplacedByToken { get; set; }
    }
}
