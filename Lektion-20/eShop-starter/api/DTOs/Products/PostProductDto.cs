using System.ComponentModel.DataAnnotations;

namespace api.DTOs.Products;

public class PostProductDto
{
    [Required]
    public string ItemNumber { get; set; } = string.Empty;
    [Required]
    public string Name { get; set; } = string.Empty;
    [Range(0.0, double.MaxValue, ErrorMessage = "Priset måste vara större än O")]
    public decimal Price { get; set; }
    [Required]
    public string ImageUrl { get; set; } = string.Empty;
    [Required]
    public string Description { get; set; } = string.Empty;
    [Required]
    public string Type { get; set; } = string.Empty;
    [Required]
    public string SupplierName { get; set; } = string.Empty;
    [Range(1, int.MaxValue, ErrorMessage = "Antal i lager måste vara minst 1")]
    public int ItemsInStock { get; set; }
}
