using eShop.Data;
using eShop.DTOs.Carts;
using eShop.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eShop.Controllers;

[Route("api/carts")]
[ApiController]
public class CartsController(EShopContext context) : ControllerBase
{
    [HttpPost()]
    public async Task<ActionResult> AddCartItem(PostCartItemDto model)
    {
        try
        {
            Customer customer = await context.Customers.FindAsync(model.CustomerId);
            if (customer is null) return BadRequest("Kunden existerar inte!");

            Product product = await context.Products.FindAsync(model.ProductId);
            if (product is null) return BadRequest("Produkten existerar inte!");

            Cart cart = await context.Carts.SingleOrDefaultAsync(c => c.CustomerId == model.CustomerId);

            if (cart is null)
            {
                cart = new Cart
                {
                    Customer = customer
                };

                context.Carts.Add(cart);
            }

            CartItem cartItem = new()
            {
                Cart = cart,
                Product = product,
                Quantity = model.Quantity,
                Price = model.Price
            };

            context.CartItems.Add(cartItem);

            await context.SaveChangesAsync();

            return StatusCode(201);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return StatusCode(500, "Något gick fel när kundvagnen skulle sparas...");
        }
    }
}

