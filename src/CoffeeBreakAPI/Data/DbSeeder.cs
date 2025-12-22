using CoffeeBreakAPI.Constants;
using CoffeeBreakAPI.Models.Auth;
using CoffeeBreakAPI.Models.Enum.Product.Cake;
using CoffeeBreakAPI.Models.Enum.Product.Coffee;
using CoffeeBreakAPI.Models.Enum.Product.Cookie;
using CoffeeBreakAPI.Models.Enum.Product.Tea;
using CoffeeBreakAPI.Models.Order;
using CoffeeBreakAPI.Models.Product;
using CoffeeBreakAPI.Models.Product.Cake;
using CoffeeBreakAPI.Models.Product.Coffee;
using CoffeeBreakAPI.Models.Product.Cookie;
using CoffeeBreakAPI.Models.Product.Tea;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CoffeeBreakAPI.Data
{
    public class DbSeeder
    {
        public static async Task SeedUsersAndRoles(IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<Guid>>>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();

            foreach (var role in new[] { Roles.ADMIN, Roles.STUFF, Roles.CLIENT })
            {
                if (!await roleManager.RoleExistsAsync(role))
                    await roleManager.CreateAsync(new IdentityRole<Guid>(role));
            }

            var adminEmail = "admin@gmail.com";
            if (await userManager.FindByEmailAsync(adminEmail) == null)
            {
                var admin = new User
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    EmailConfirmed = true,
                    FirstName = "Ema",
                    LastName = "Miler",
                    Pesel = "00000000001"
                };

                await userManager.CreateAsync(admin, "Test1234$");
                await userManager.AddToRoleAsync(admin, Roles.ADMIN);
            }
        }

        public static async Task SeedProducts(IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            if (await context.Products.AnyAsync())
                return;

            var products = new List<Product>
            {
                new Coffee(
                    "Espresso",
                    "Strong Italian espresso",
                    Money.PLN(8),
                    "/images/coffee/espresso.jpg",
                    CoffeeType.Espresso,
                    CoffeeSize.Small,
                    false
                ),
                new Coffee(
                    "Latte",
                    "Coffee with milk",
                    Money.PLN(14),
                    "/images/coffee/latte.jpg",
                    CoffeeType.MilkBased,
                    CoffeeSize.Large,
                    true
                ),
                new Coffee(
                    "Americano",
                    "Espresso with water",
                    Money.PLN(9),
                    "/images/coffee/americano.jpg",
                    CoffeeType.Espresso,
                    CoffeeSize.Medium,
                    false
                ),
                new Coffee(
                    "Cappuccino",
                    "Espresso with foam",
                    Money.PLN(13),
                    "/images/coffee/cappuccino.jpg",
                    CoffeeType.MilkBased,
                    CoffeeSize.Medium,
                    true
                ),

                new Tea(
                    "Green Tea",
                    "Refreshing green tea",
                    Money.PLN(10),
                    "/images/tea/green.jpg",
                    TeaType.Green,
                    false
                ),
                new Tea(
                    "Black Tea",
                    "Classic black tea",
                    Money.PLN(9),
                    "/images/tea/black.jpg",
                    TeaType.Black,
                    false
                ),
                new Tea(
                    "Fruit Tea",
                    "Sweet fruit tea",
                    Money.PLN(12),
                    "/images/tea/fruit.jpg",
                    TeaType.Fruit,
                    true
                ),
                new Tea(
                    "Herbal Tea",
                    "Herbal infusion",
                    Money.PLN(11),
                    "/images/tea/herbal.jpg",
                    TeaType.Herbal,
                    true
                ),

                new Cake(
                    "Cheesecake",
                    "Classic cheesecake",
                    Money.PLN(18),
                    "/images/cake/cheesecake.jpg",
                    CakeType.Cheesecake,
                    false,
                    420
                ),
                new Cake(
                    "Chocolate Cake",
                    "Dark chocolate cake",
                    Money.PLN(20),
                    "/images/cake/chocolate.jpg",
                    CakeType.Chocolate,
                    false,
                    480
                ),
                new Cake(
                    "Fruit Cake",
                    "Cake with fruits",
                    Money.PLN(17),
                    "/images/cake/fruit.jpg",
                    CakeType.Fruit,
                    false,
                    390
                ),
                new Cake(
                    "Gluten Free Cake",
                    "No gluten cake",
                    Money.PLN(22),
                    "/images/cake/glutenfree.jpg",
                    CakeType.Vegan,
                    true,
                    350
                ),

                new Cookie(
                    "Chocolate Cookie",
                    "Cookie with chocolate",
                    Money.PLN(6),
                    "/images/cookie/chocolate.jpg",
                    CookieType.Chocolate,
                    true
                ),
                new Cookie(
                    "Oatmeal Cookie",
                    "Healthy oatmeal cookie",
                    Money.PLN(5),
                    "/images/cookie/oatmeal.jpg",
                    CookieType.Oatmeal,
                    false
                ),
                new Cookie(
                    "Butter Cookie",
                    "Classic butter cookie",
                    Money.PLN(4),
                    "/images/cookie/butter.jpg",
                    CookieType.Butter,
                    false
                ),
                new Cookie(
                    "Filled Cookie",
                    "Cookie with filling",
                    Money.PLN(7),
                    "/images/cookie/filled.jpg",
                    CookieType.Chocolate,
                    true
                )
            };

            await context.Products.AddRangeAsync(products);
            await context.SaveChangesAsync();
        }

    }
}
