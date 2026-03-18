using System.ComponentModel.DataAnnotations;

namespace eShop.DTOs.Customers;

public abstract class BaseCustomerDto
{
    [Required(ErrorMessage = "Firstname is required")]
    public string FirstName { get; set; }
    [Required(ErrorMessage = "Lastname is required")]
    public string LastName { get; set; }
}
