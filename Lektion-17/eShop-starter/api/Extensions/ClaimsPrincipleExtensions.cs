using System.Security.Authentication;
using System.Security.Claims;
using core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace api.Extensions;

public static class ClaimsPrincipleExtensions
{
    public static string GetUserEmail(this ClaimsPrincipal user)
    {
        var email = user.FindFirstValue(ClaimTypes.Email)
            ?? throw new AuthenticationException("Email not found");
        return email;
    }

    public static async Task<AppUser> GetUserByEmail(this UserManager<AppUser> userManager, ClaimsPrincipal user)
    {
        var found = await userManager.Users.FirstOrDefaultAsync(c => c.Email == user.GetUserEmail())
            ?? throw new AuthenticationException("User not found");

        return found;
    }

    public static async Task<AppUser> GetUserByEmailWithAddress(this UserManager<AppUser> userManager, ClaimsPrincipal user)
    {
        var found = await userManager.Users
            .Include(c => c.Address)
            .FirstOrDefaultAsync(c => c.Email == user.GetUserEmail())
            ?? throw new AuthenticationException("User not found");

        return found;
    }
}
