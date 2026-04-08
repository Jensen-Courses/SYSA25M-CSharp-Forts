using eShop.Data;
using eShop.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eShop.Controllers;

[Route("api/payments")]
[ApiController]
public class PaymentsController(EShopContext context) : ControllerBase
{
    [HttpPost()]
    public async Task<ActionResult> Pay([FromQuery] int customerId)
    {
        // 1.   Hämta kunden ifrån databasen...
        Customer customer = await context.Customers.FindAsync(customerId);
        if (customer is null) return BadRequest("Kund finns inte!!!");

        // 2.   Hämta kundvagnen för kunden...
        Cart cart = await context.Carts
            .Include(c => c.CartItems)
            .SingleOrDefaultAsync(c => c.CustomerId == customerId);
        if (cart is null) return BadRequest("Finns ingen kundvagn för kunden...");

        // 3.   Hämta ut produkterna ifrån kundvagnen...
        //      skapa orderitem för varje produkt...
        List<OrderItem> items = [.. cart.CartItems.Select(o => new OrderItem{
            ProductId = o.ProductId,
            Quantity = o.Quantity,
            Price = o.Price
        })];

        // 4.   Skapa en SalesOrder...
        SalesOrder order = new()
        {
            Customer = customer,
            OrderItems = items
        };

        context.SalesOrders.Add(order);

        // 5.   Radera kundvagnen...
        context.Carts.Remove(cart);

        // 6. Spara skiten...
        await context.SaveChangesAsync();
        return StatusCode(201);
    }
}

