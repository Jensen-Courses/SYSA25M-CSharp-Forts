namespace core.Entities;

public class Cart
{
    public required string Id { get; set; }
    public List<CartItem> Items { get; set; } = [];
}
