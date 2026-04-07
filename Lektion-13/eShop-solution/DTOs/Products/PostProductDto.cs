namespace eShop.DTOs.Products;

public class PostProductDto
{
    public string SupplierName { get; set; }
    public string ItemNumber { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public string ImageUrl { get; set; }
    public string Description { get; set; }
}
