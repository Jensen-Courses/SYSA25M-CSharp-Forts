using System.Text.Json;
using core.Entities;
using core.Interfaces;
using StackExchange.Redis;

namespace infrastructure.Services;

public class CartService(IConnectionMultiplexer redis) : ICartService
{
    private readonly IDatabase _database = redis.GetDatabase();
    public async Task<bool> DeleteCartAsync(string key)
    {
        return await _database.KeyDeleteAsync(key);
    }

    public async Task<Cart?> GetCartAsync(string key)
    {
        var data = await _database.StringGetAsync(key);
        return data.IsNullOrEmpty ? null : JsonSerializer.Deserialize<Cart>((byte[])data!);
    }

    public async Task<Cart?> SetCartAsync(Cart cart)
    {
        var data = await _database.StringSetAsync(cart.Id, JsonSerializer.Serialize(cart), TimeSpan.FromDays(30));
        if (!data) return null;

        return await GetCartAsync(cart.Id);
    }
}
