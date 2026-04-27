namespace core.Entities;

public class CartItem
{
    public required string ProductId { get; set; }
    public required string ProductName { get; set; }
    public double Price { get; set; }
    public int Quantity { get; set; }
}
