using System.ComponentModel.DataAnnotations;

namespace api.DTOs.Orders;

public class PostDeliveryMethodDto
{
    [Required]
    public required string ShortName { get; set; }
    [Required]
    public required string DeliveryTime { get; set; }
    [Required]
    public required string Description { get; set; }
    [Required]
    public decimal Price { get; set; } = 0.00M;
}
