using System.Text.Json;
using eShop.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace eShop.Data;

public class SeedDatabase(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
{
    private static readonly JsonSerializerOptions options = new()
    {
        PropertyNameCaseInsensitive = true
    };

    public async Task InitDb(WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<EShopContext>();

        context.Database.Migrate();

        await SeedUsers();
        await SeedProducts(context);
    }

    public async Task SeedUsers()
    {
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

    public async Task SeedProducts(EShopContext context)
    {
        // Titta om vi redan har leverantörer i databasen...
        if (context.Products.Any()) return;

        // Läsa in json filen och skapa Supplier objekt...
        var json = File.ReadAllText("Data/Json/products.json");
        var products = JsonSerializer.Deserialize<List<Product>>(json, options);

        if (products is not null && products.Count > 0)
        {
            await context.Products.AddRangeAsync(products);
            await context.SaveChangesAsync();
        }
    }
}
