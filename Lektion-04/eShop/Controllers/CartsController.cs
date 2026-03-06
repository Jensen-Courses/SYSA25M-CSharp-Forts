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
            // 1. Kontrollera så att kunden existerar...
            Customer customer = await context.Customers.FindAsync(model.CustomerId);
            if (customer is null) return BadRequest("Kunden existerar inte!");

            // 2. Kontrollera så att produkten existerar...
            Product product = await context.Products.FindAsync(model.ProductId);
            if (product is null) return BadRequest("Produkten existerar inte!");

            // 3. Kontrollerar om kunden har en existerande kundvagn...
            Cart cart = await context.Carts.SingleOrDefaultAsync(c => c.CustomerId == model.CustomerId);

            // 3.1 Om kundvagnen inte finns, så skapar vi den...
            if (cart is null)
            {
                cart = new Cart
                {
                    Customer = customer
                };

                // 3.2 Lägg kundvagnen i EF övervakning...
                context.Carts.Add(cart);
            }

            // 4. Skapa ett cartItem...
            CartItem cartItem = new()
            {
                Cart = cart,
                Product = product,
                Quantity = model.Quantity,
                Price = model.Price
            };

            // 4.1 Lägg till cartItem till EF övervakning...
            context.CartItems.Add(cartItem);

            // 5. Spara ner både Cart och CartItems till databasen...
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

