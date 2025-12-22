using CoffeeBreakAPI.Models.Enum.Product.Cookie;

namespace CoffeeBreakAPI.Dtos.Product
{
    public class CookieDto: ProductDto
    {
        public CookieType CookieType { get; set; }

        public bool HasChocolate { get; set; }
    }
}
