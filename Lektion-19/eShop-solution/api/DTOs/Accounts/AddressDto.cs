using System.ComponentModel.DataAnnotations;

namespace api.DTOs.Accounts;

public class AddressDto
{
    [Required]
    public string AddressLine { get; set; } = string.Empty;
    [Required]
    public string PostalCode { get; set; } = string.Empty;
    [Required]
    public string City { get; set; } = string.Empty;
}
