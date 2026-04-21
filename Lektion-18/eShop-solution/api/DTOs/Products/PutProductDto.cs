using System.ComponentModel.DataAnnotations;

namespace api.DTOs.Products;

public class PutProductDto : PostProductDto
{
    [Required]
    public required string Id { get; set; }
}
