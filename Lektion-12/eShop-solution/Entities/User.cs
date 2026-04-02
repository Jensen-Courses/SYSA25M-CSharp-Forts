using Microsoft.AspNetCore.Identity;

namespace eShop.Entities;

public class User : IdentityUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
}
