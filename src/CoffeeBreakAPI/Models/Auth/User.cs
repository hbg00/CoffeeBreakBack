using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace CoffeeBreakAPI.Models.Auth
{
    public class User : IdentityUser<Guid>
    {
        [Required]
        public string FirstName { get; set; } = string.Empty;
        [Required]
        public string LastName { get; set; } = string.Empty;
    
        public string? Pesel { get; set; } = string.Empty;
    }
}
