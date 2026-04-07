namespace eShop.Entities;

public class Product : BaseEntity
{
    public required string ItemNumber { get; set; }
    public required string ProductName { get; set; }
    public required string ImageUrl { get; set; }
    public decimal Price { get; set; }
    public required string Description { get; set; }
    public required string Type { get; set; }
    public required string Brand { get; set; }
    public int ItemsInStock { get; set; }
}
