using System.Reflection;
using System.Text.Json;
using core.Entities;

namespace infrastructure.Data;

public class SeedDatabase()
{
    private static readonly JsonSerializerOptions options = new()
    {
        PropertyNameCaseInsensitive = true
    };

    public static async Task SeedSuppliers(EShopContext contex)
    {
        var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        if (contex.Suppliers.Any()) return;

        var json = File.ReadAllText(path + @"/Data/Json/suppliers.json");
        var suppliers = JsonSerializer.Deserialize<List<Supplier>>(json, options);

        if (suppliers is not null)
        {
            await contex.Suppliers.AddRangeAsync(suppliers);
            await contex.SaveChangesAsync();
        }
    }

    public static async Task SeedProducts(EShopContext context)
    {
        var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        // Titta om vi redan har leverantörer i databasen...
        if (context.Products.Any()) return;

        // Läsa in json filen och skapa Supplier objekt...
        var json = File.ReadAllText(path + @"/Data/Json/products.json");
        var products = JsonSerializer.Deserialize<List<Product>>(json, options);

        if (products is not null && products.Count > 0)
        {
            await context.Products.AddRangeAsync(products);
            await context.SaveChangesAsync();
        }
    }
}
