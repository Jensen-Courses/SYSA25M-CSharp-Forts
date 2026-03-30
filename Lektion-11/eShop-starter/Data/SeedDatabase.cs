using eShop.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace eShop.Data;

public class SeedDatabase(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
{
    public async Task InitDb(WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        await SeedData(scope.ServiceProvider.GetRequiredService<EShopContext>());
    }

    public async Task SeedData(EShopContext context)
    {
        context.Database.Migrate();

        var role = await roleManager.FindByNameAsync("Admin");
        if (role is null)
        {
            await roleManager.CreateAsync(new IdentityRole { Name = "Admin" });
        }

        var admin = await userManager.FindByNameAsync("Admin");
        if (admin is null)
        {
            var user = new User
            {
                Email = "admin@mail.com",
                UserName = "Admin",
                FirstName = "Admin",
                LastName = "Adminsson"
            };
            await userManager.CreateAsync(user, "Pa$$w0rd");
            await userManager.AddToRoleAsync(user, "Admin");
        }
    }
}
