namespace api.DTOs.Products;

public class ProductDto : ListProductsDto
{
    public string? ImageUrl { get; set; }
    public string? Description { get; set; }
}
