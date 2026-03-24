namespace eShop.DTOs.Auth;

public class PostRegisterUser : AuthBase
{
    public string Phone { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
}
