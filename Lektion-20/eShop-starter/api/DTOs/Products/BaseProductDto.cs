using core.Entities;

namespace api.DTOs.Products;

public class BaseProductDto
{
    public required string ItemNumber { get; set; }
    public string? Name { get; set; }
    public required string SupplierName { get; set; }
    public decimal Price { get; set; }
}
