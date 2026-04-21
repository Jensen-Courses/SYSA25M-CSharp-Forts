using core.Entities;
using core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

public class CartController(ICartService cartService) : ApiBaseController
{
    [HttpGet()]
    public async Task<ActionResult> GetCart(string id)
    {
        var cart = await cartService.GetCartAsync(id);
        return Ok(cart ?? new Cart { Id = id });
    }

    [HttpPost()]
    public async Task<ActionResult<Cart>> UpdateCart(Cart cart)
    {
        var updated = await cartService.SetCartAsync(cart);
        if (updated is null) return BadRequest("Problem med kundvagnen");
        return cart;
    }
}
