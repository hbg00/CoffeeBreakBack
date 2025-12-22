using CoffeeBreakAPI.Data;
using CoffeeBreakAPI.Interfaces;
using CoffeeBreakAPI.Models.Enum.Product.Cake;
using CoffeeBreakAPI.Models.Enum.Product.Coffee;
using CoffeeBreakAPI.Models.Enum.Product.Cookie;
using CoffeeBreakAPI.Models.Product;
using CoffeeBreakAPI.Models.Product.Cake;
using CoffeeBreakAPI.Models.Product.Coffee;
using CoffeeBreakAPI.Models.Product.Tea;
using CoffeeBreakAPI.Models.Product.Cookie;
using Microsoft.EntityFrameworkCore;
using CoffeeBreakAPI.Models.Enum.Product.Tea;

namespace CoffeeBreakAPI.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;

        public ProductRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IReadOnlyList<Product>> GetAllAsync()
        {
            return await _context.Products
                .Where(p => p.IsAvailable)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Product?> GetByIdAsync(Guid id)
        {
            return await _context.Products
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == id && p.IsAvailable);
        }

        public async Task<IReadOnlyList<Coffee>> GetCoffeesAsync()
        {
            return await _context.Products
                .OfType<Coffee>()
                .Where(c => c.IsAvailable)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IReadOnlyList<Coffee>> GetCoffeesByTypeAsync(CoffeeType type)
        {
            return await _context.Products
                .OfType<Coffee>()
                .Where(c => c.Type == type && c.IsAvailable)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IReadOnlyList<Tea>> GetTeasAsync()
        {
            return await _context.Products
                .OfType<Tea>()
                .Where(t => t.IsAvailable)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IReadOnlyList<Tea>> GetTeasByTypeAsync(TeaType type)
        {
            return await _context.Products
                .OfType<Tea>()
                .Where(t => t.Type == type && t.IsAvailable)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IReadOnlyList<Cake>> GetCakesAsync()
        {
            return await _context.Products
                .OfType<Cake>()
                .Where(c => c.IsAvailable)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IReadOnlyList<Cake>> GetCakesByTypeAsync(CakeType type)
        {
            return await _context.Products
                .OfType<Cake>()
                .Where(c => c.Type == type && c.IsAvailable)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IReadOnlyList<Cookie>> GetCookiesAsync()
        {
            return await _context.Products
                .OfType<Cookie>()
                .Where(c => c.IsAvailable)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IReadOnlyList<Cookie>> GetCookiesByTypeAsync(CookieType type)
        {
            return await _context.Products
                .OfType<Cookie>()
                .Where(c => c.Type == type && c.IsAvailable)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
