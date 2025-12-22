using CoffeeBreakAPI.Models.Enum.Product.Cake;
using CoffeeBreakAPI.Models.Enum.Product.Coffee;
using CoffeeBreakAPI.Models.Enum.Product.Cookie;
using CoffeeBreakAPI.Models.Product;
using CoffeeBreakAPI.Models.Product.Cake;
using CoffeeBreakAPI.Models.Product.Coffee;
using CoffeeBreakAPI.Models.Product.Tea;
using CoffeeBreakAPI.Models.Product.Cookie;
using CoffeeBreakAPI.Models.Enum.Product.Tea;

namespace CoffeeBreakAPI.Interfaces
{
    public interface IProductRepository
    {
        Task<IReadOnlyList<Product>> GetAllAsync();
        Task<Product?> GetByIdAsync(Guid id);

        Task<IReadOnlyList<Coffee>> GetCoffeesAsync();
        Task<IReadOnlyList<Coffee>> GetCoffeesByTypeAsync(CoffeeType type);

        Task<IReadOnlyList<Tea>> GetTeasAsync();
        Task<IReadOnlyList<Tea>> GetTeasByTypeAsync(TeaType type);

        Task<IReadOnlyList<Cake>> GetCakesAsync();
        Task<IReadOnlyList<Cake>> GetCakesByTypeAsync(CakeType type);

        Task<IReadOnlyList<Cookie>> GetCookiesAsync();
        Task<IReadOnlyList<Cookie>> GetCookiesByTypeAsync(CookieType type);
    }
}
