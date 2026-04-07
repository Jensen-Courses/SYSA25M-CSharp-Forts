namespace eShop.DTOs.Products;

public class PostProductDto
{
    public int SupplierId { get; set; }
    public string ItemNumber { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public string ImageUrl { get; set; }
    public string Description { get; set; }
}
