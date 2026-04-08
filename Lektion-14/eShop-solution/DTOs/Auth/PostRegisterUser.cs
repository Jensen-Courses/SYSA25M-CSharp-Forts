namespace eShop.DTOs.Auth;

public class PostRegisterUser : AuthBase
{
    public string? Phone { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
}
