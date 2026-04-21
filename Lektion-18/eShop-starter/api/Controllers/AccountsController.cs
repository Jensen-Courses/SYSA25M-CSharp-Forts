using api.DTOs.Accounts;
using api.Extensions;
using core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

public class AccountsController(SignInManager<AppUser> signInManager) : ApiBaseController
{
    [HttpPost("register")]
    public async Task<ActionResult> Register(CreateUserDto model)
    {
        var user = new AppUser
        {
            FirstName = model.FirstName,
            LastName = model.LastName,
            Email = model.Email,
            UserName = model.Email
        };

        var result = await signInManager.UserManager.CreateAsync(user, model.Password);

        if (!result.Succeeded) return BadRequest(result.Errors);

        return StatusCode(201);
    }

    [HttpPost("logout")]
    public async Task<ActionResult> Logout()
    {
        await signInManager.SignOutAsync();
        return NoContent();
    }

    [HttpGet("user-info")]
    public async Task<ActionResult> UserInfo()
    {
        if (User.Identity?.IsAuthenticated == false) return NoContent();

        var user = await signInManager.UserManager.GetUserByEmailWithAddress(User);

        return Ok(new
        {
            user.FirstName,
            user.LastName,
            user.Email,
            Address = user.Address?.AddressLine,
            user.Address?.PostalCode,
            user.Address?.City
        });
    }

    [HttpPost("address")]
    public async Task<ActionResult> CreateOrUpdateAddress(AddressDto model)
    {
        var user = await signInManager.UserManager.GetUserByEmailWithAddress(User);

        if (user.Address is null)
        {
            var address = new Address { AddressLine = model.AddressLine, PostalCode = model.PostalCode, City = model.City };
            user.Address = address;
        }
        else
        {
            user.Address.AddressLine = model.AddressLine;
            user.Address.PostalCode = model.PostalCode;
            user.Address.City = model.City;
        }

        await signInManager.UserManager.UpdateAsync(user);

        return Ok();
    }
}
