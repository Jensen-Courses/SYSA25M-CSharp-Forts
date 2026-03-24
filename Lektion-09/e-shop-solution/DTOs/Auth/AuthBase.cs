namespace eShop.DTOs.Auth;

public class AuthBase
{
    // Kommer att fungera som både e-post men också som användarnamn i systemet...
    public string Email { get; set; }
    public string Password { get; set; }
}
