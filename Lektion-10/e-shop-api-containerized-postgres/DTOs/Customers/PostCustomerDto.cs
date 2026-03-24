using System.ComponentModel.DataAnnotations;

namespace eShop.DTOs.Customers;

public class PostCustomerDto : BaseCustomerDto
{
    [Required(ErrorMessage = "Email is required")]
    public string Email { get; set; }
}
