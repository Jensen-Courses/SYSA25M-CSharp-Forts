using System.ComponentModel.DataAnnotations;
using core.Entities.Orders;

namespace api.DTOs.Orders;

public class PostOrderDto
{
    [Required]
    public string CartId { get; set; } = string.Empty;
    [Required]
    public required string DeliveryMethodId { get; set; }
    [Required]
    public ShippingAddress ShippingAddress { get; set; } = null!;
    [Required]
    public PaymentInfo PaymentInfo { get; set; } = null!;
}
