using CoffeeBreakAPI.Constants;
using CoffeeBreakAPI.Models.Auth;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;

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
                    UserName = "admin@gmail.com",
                    Email = adminEmail,
                    EmailConfirmed = true,
                    FirstName = "Ema",
                    LastName = "Miler",
                    Pesel = "00000000001"
                };
                var result = await userManager.CreateAsync(admin, "Test1234$");
                if (!result.Succeeded)
                    throw new Exception(string.Join("; ", result.Errors.Select(e => e.Description)));

                await userManager.AddToRoleAsync(admin, Roles.ADMIN);
            }

            var clientEmail = "client@gmail.com";
            if (await userManager.FindByEmailAsync(clientEmail) == null)
            {
                var client = new User
                {
                    UserName = "client@gmail.com",
                    Email = clientEmail,
                    EmailConfirmed = true,
                    FirstName = "Test",
                    LastName = "Test"
                };
                var result = await userManager.CreateAsync(client, "Test1234$");
                if (!result.Succeeded)
                    throw new Exception(string.Join("; ", result.Errors.Select(e => e.Description)));

                await userManager.AddToRoleAsync(client, Roles.CLIENT);
            }
        }
    }
}
