namespace eShop.Entities;

public record OrderItem
{
    // Sammansatt primärnyckel SalesOrderId och ProductId
    public int SalesOrderId { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    // Navigeringsegenskaper...
    public Product Product { get; set; }
    public SalesOrder SalesOrder { get; set; }
}
