using CoffeeBreakAPI.Models.Enum.Product.Cookie;
using CoffeeBreakAPI.Models.Order;

namespace CoffeeBreakAPI.Models.Product.Cookie
{
    public class Cookie : Product
    {
        public CookieType Type { get; private set; }
        public bool HasChocolate { get; private set; }

        private Cookie() { }

        public Cookie(
            string name,
            string description,
            Money price,
            string imageUrl,
            CookieType type,
            bool hasChocolate
        ) : base(name, description, price, imageUrl)
        {
            Type = type;
            HasChocolate = hasChocolate;
        }
    }
}
