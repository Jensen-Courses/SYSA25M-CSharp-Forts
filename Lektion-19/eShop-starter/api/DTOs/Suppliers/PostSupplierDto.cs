using System.ComponentModel.DataAnnotations;

namespace api.DTOs.Suppliers;

public class PostSupplierDto
{
    [Required]
    public string Name { get; set; } = string.Empty;
    [Required]
    public string Phone { get; set; } = string.Empty;
    [Required]
    public string WebSite { get; set; } = string.Empty;
    [Required]
    public int CustomerNumber { get; set; }
}
