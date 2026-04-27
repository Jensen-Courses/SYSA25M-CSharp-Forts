using core.Entities;

namespace core.Interfaces;

public interface ICartService
{
    Task<Cart?> GetCartAsync(string key);
    Task<Cart?> SetCartAsync(Cart cart);
    Task<bool> DeleteCartAsync(string key);
}
