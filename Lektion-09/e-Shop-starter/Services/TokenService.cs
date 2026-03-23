using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using eShop.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace eShop.Services;

public class TokenService(UserManager<User> userManager, IConfiguration config)
{
    public async Task<string> CreateToken(User user)
    {
        // 1. Skapa en payload. Ett paket med användarens uppgifter
        List<Claim> claims = [
            new(ClaimTypes.Email,user.Email),
            new(ClaimTypes.Name, user.UserName),
            new("FirstName",user.FirstName),
            new("LastName", user.LastName),
            new("Id",user.Id)
        ];

        // 2. Lägga till roll/roller som användaren påstår tillhöra.
        var roles = await userManager.GetRolesAsync(user);
        foreach (var role in roles)
        {
            claims.Add(new(ClaimTypes.Role, role));
        }

        /***********************************************************/
        //      Skapa signaturen
        /***********************************************************/
        // 3. Hämta den hemliga nyckeln...
        var tokenKey = config["tokenSettings:tokenKey"];
        // 4. Skapa en krypteringsnyckel...
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenKey));
        // 5. Skapa "credentials" med en erkänd säker algoritm...
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

        /***********************************************************/
        //      Skapa biljetten (JWT)
        /***********************************************************/
        // 6. Skapa JWT...
        var options = new JwtSecurityToken(
            issuer: null,
            audience: null,
            claims: claims,
            expires: DateTime.Now.AddDays(5),
            signingCredentials: credentials
        );

        // 7. Returnera biljetten(JWT)...
        return new JwtSecurityTokenHandler().WriteToken(options);
    }
}
