namespace eShop.Entities;

public class CartItem
{
    public int CartId { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }

    // Navigeringsegenskaper...
    public Product Product { get; set; }
    public Cart Cart { get; set; }
}
