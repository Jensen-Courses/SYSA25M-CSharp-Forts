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

            Cart cart = await context.Carts.Include(c => c.CartItems).SingleOrDefaultAsync(c => c.CustomerId == model.CustomerId);

            if (cart is null)
            {
                cart = new Cart
                {
                    Customer = customer
                };

                context.Carts.Add(cart);
            }

            if (cart.CartItems is not null)
            {
                var foundItem = cart.CartItems.SingleOrDefault(i => i.ProductId == model.ProductId);
                if (foundItem is not null)
                {
                    foundItem.Quantity = model.Quantity;
                }
                else
                {
                    CartItem cartItem = new()
                    {
                        Cart = cart,
                        Product = product,
                        Quantity = model.Quantity,
                        Price = model.Price
                    };

                    context.CartItems.Add(cartItem);
                }
            }
            else
            {
                CartItem cartItem = new()
                {
                    Cart = cart,
                    Product = product,
                    Quantity = model.Quantity,
                    Price = model.Price
                };

                context.CartItems.Add(cartItem);
            }

            await context.SaveChangesAsync();

            return CreatedAtAction(nameof(FindCart),
                new { id = cart.CartId },
                new { cart.CartId, cart.CreatedDate });
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return StatusCode(500, "Något gick fel när kundvagnen skulle sparas...");
        }
    }

    [HttpGet()]
    public async Task<ActionResult> ListAllCarts()
    {
        var carts = await context.Carts
        .Select(cart => new
        {
            cart.CartId,
            cart.CustomerId,
            cart.Customer.FirstName,
            cart.Customer.LastName,
            cart.Customer.Email,
            CreatedDate = cart.CreatedDate.ToShortDateString()
        })
        .ToListAsync();

        return Ok(new { Success = true, StatusCode = 200, Items = carts.Count, Data = carts });
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> FindCart(int id)
    {
        Cart cart = await context.Carts
            .Include(c => c.Customer)
            .SingleOrDefaultAsync(c => c.CartId == id);

        var data = new
        {
            cart.CartId,
            cart.CustomerId,
            cart.Customer.FirstName,
            cart.Customer.LastName,
            cart.Customer.Email,
            CreatedDate = cart.CreatedDate.ToShortDateString()
        };

        return Ok(new { Success = true, StatusCode = 200, Items = "Not defined", Data = data });
    }
}

