using CoffeeBreakAPI.Models.Auth;
using CoffeeBreakAPI.Models.Enum.Product;
using CoffeeBreakAPI.Models.Product;
using CoffeeBreakAPI.Models.Product.Cake;
using CoffeeBreakAPI.Models.Product.Coffee;
using CoffeeBreakAPI.Models.Product.Cookie;
using CoffeeBreakAPI.Models.Product.Tea;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CoffeeBreakAPI.Data
{
    public class AppDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products => Set<Product>();

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Product>()
                .OwnsOne(p => p.Price, money =>
                {
                    money.Property(m => m.Amount)
                        .HasColumnName("PriceAmount")
                        .HasPrecision(18, 2);

                    money.Property(m => m.Currency)
                        .HasColumnName("PriceCurrency")
                        .HasMaxLength(3);
                });

            builder.Entity<Product>()
                .HasDiscriminator<string>("ProductKind")
                .HasValue<Coffee>("Coffee")
                .HasValue<Tea>("Tea")
                .HasValue<Cake>("Cake")
                .HasValue<Cookie>("Cookie");
        }
    }
}
