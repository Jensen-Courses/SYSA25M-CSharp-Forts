using System.Text.Json;
using core.Entities;

namespace infrastructure.Data;

public class SeedDatabase()
{
    private static readonly JsonSerializerOptions options = new()
    {
        PropertyNameCaseInsensitive = true
    };

    public static async Task SeedProducts(EShopContext context)
    {
        // Titta om vi redan har leverantörer i databasen...
        if (context.Products.Any()) return;

        // Läsa in json filen och skapa Supplier objekt...
        var json = File.ReadAllText("../infrastructure/Data/Json/products.json");
        var products = JsonSerializer.Deserialize<List<Product>>(json, options);

        if (products is not null && products.Count > 0)
        {
            await context.Products.AddRangeAsync(products);
            await context.SaveChangesAsync();
        }
    }
}
