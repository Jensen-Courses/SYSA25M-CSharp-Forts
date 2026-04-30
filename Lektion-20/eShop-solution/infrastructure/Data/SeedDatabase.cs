using System.Reflection;
using System.Text.Json;
using core.Entities;
using Microsoft.AspNetCore.Identity;

namespace infrastructure.Data;

public class SeedDatabase()
{
    private static readonly JsonSerializerOptions options = new()
    {
        PropertyNameCaseInsensitive = true
    };

    public static async Task SeedDataAsync(EShopContext context, UserManager<AppUser> userManager)
    {
        if (!userManager.Users.Any(c => c.UserName == "admin@mail.com"))
        {
            var user = new AppUser
            {
                UserName = "admin@mail.com",
                Email = "admin@mail.com"
            };

            await userManager.CreateAsync(user, "Pa$$w0rd");
            await userManager.AddToRoleAsync(user, "Admin");
        }

        var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        if (context.Suppliers.Any()) return;

        var json = File.ReadAllText(path + @"/Data/Json/suppliers.json");
        var suppliers = JsonSerializer.Deserialize<List<Supplier>>(json, options);

        if (suppliers is not null)
        {
            await context.Suppliers.AddRangeAsync(suppliers);
            await context.SaveChangesAsync();
        }

        if (context.Products.Any()) return;

        json = File.ReadAllText(path + @"/Data/Json/products.json");
        var products = JsonSerializer.Deserialize<List<Product>>(json, options);

        if (products is not null && products.Count > 0)
        {
            await context.Products.AddRangeAsync(products);
            await context.SaveChangesAsync();
        }
    }
    // public static async Task SeedSuppliers(EShopContext contex)
    // {
    //     var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
    //     if (contex.Suppliers.Any()) return;

    //     var json = File.ReadAllText(path + @"/Data/Json/suppliers.json");
    //     var suppliers = JsonSerializer.Deserialize<List<Supplier>>(json, options);

    //     if (suppliers is not null)
    //     {
    //         await contex.Suppliers.AddRangeAsync(suppliers);
    //         await contex.SaveChangesAsync();
    //     }
    // }

    // public static async Task SeedProducts(EShopContext context)
    // {
    //     var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
    //     // Titta om vi redan har leverantörer i databasen...
    //     if (context.Products.Any()) return;

    //     // Läsa in json filen och skapa Supplier objekt...
    //     var json = File.ReadAllText(path + @"/Data/Json/products.json");
    //     var products = JsonSerializer.Deserialize<List<Product>>(json, options);

    //     if (products is not null && products.Count > 0)
    //     {
    //         await context.Products.AddRangeAsync(products);
    //         await context.SaveChangesAsync();
    //     }
    // }
}
